using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OctoEvents.CrossCutting.Interfaces.Handling.Commands;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.Domain.Entities;
using OctoEvents.Domain.Extensions;
using OctoEvents.Domain.Operations.Commands;
using OctoEvents.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OctoEvents.Application.Handlers.Commands
{
    public class SaveIssueInteractionCommandHandler : ISaveIssueInteractionCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<EventViewModel> _validator;
        private readonly IMapper _mapper;
        private readonly IIssueRepository _issueRepository;
        private readonly IRepositoriesRepository _repositoriesRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<SaveIssueInteractionCommandHandler> _logger;

        public SaveIssueInteractionCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<EventViewModel> validator,
            IMapper mapper,
            IIssueRepository issueRepository,
            IRepositoriesRepository repositoriesRepository,
            IUserRepository userRepository,
            ILogger<SaveIssueInteractionCommandHandler> logger
            )
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _issueRepository = issueRepository;
            _repositoriesRepository = repositoriesRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(SaveIssueInteractionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SaveIssueInteractionCommand received. Validating parameters.");

            var validation = await _validator.ValidateAsync(request.Event)!;

            if (!validation.IsValid)
            {
                _logger.LogWarning("Received command is not valid.");
                return validation;
            }

            _logger.LogDebug($"Received payload: \n{JsonConvert.SerializeObject(request.Event)}");

            _logger.LogDebug("Starting transaction.");
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                _logger.LogDebug("Searching issue by external ID");
                var issue = await _issueRepository.GetByExternalIdAsync(request.Event.Issue!.ExternalId, cancellationToken);

                var userExternalIds = new[]
                {
                    request.Event.Issue.User!.ExternalId,
                    request.Event.Repository!.Owner!.ExternalId,
                    request.Event.Sender!.ExternalId
                }.Distinct().ToList();

                var userDictionary = await _userRepository.GetAllByExternalIdsAsync(userExternalIds, cancellationToken);

                if (issue == null)
                {
                    var result = await CreateIssueAsync(request.Event, userDictionary, cancellationToken);

                    if (!result.Item2.IsValid)
                    {
                        return result.Item2;
                    }

                    issue = result.Item1 as Issue;
                }
                else
                {
                    validation = await UpdateIssueAsync(issue, request.Event, userDictionary, cancellationToken);

                    if (!validation.IsValid)
                    {
                        return validation;
                    }
                }

                _logger.LogInformation("Saving changes to database.");
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Committing transaction.");
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                var message = "Error while trying to save issue event.";
                _logger.LogError(ex, message);

                validation.Errors.Add(new ValidationFailure(string.Empty, message));

                _logger.LogWarning("Rolling back transaction.");
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            }
            finally
            {
                if (!_unitOfWork.IsCompleted)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                }
            }

            _logger.LogDebug("SaveIssueInteractionCommand execution finished.");

            return validation;
        }

        private async Task<(Issue?, ValidationResult)> CreateIssueAsync(EventViewModel @event, Dictionary<long, User> userDictionary, CancellationToken cancellationToken = default)
        {
            var validation = new ValidationResult();
            _logger.LogDebug("Issue not found. Creating issue from received event.");

            var issue = _mapper.Map<Issue>(@event).FillAuditValuesForCreation();

            _logger.LogInformation("Updating user data");
            validation = UpdateRelatedUsers(issue, userDictionary);

            if (!validation.IsValid)
            {
                _logger.LogWarning($"User data update failed: \n{validation}");

                return (null, validation)!;
            }

            validation = await UpdateRepositoryAsync(issue);

            if (!validation.IsValid)
            {
                _logger.LogWarning($"Repository update failed: \n {validation}");
                return (null, validation);
            }

            StandardizeCommonUsers(issue);

            _logger.LogInformation("Saving issue to database.");

            if (issue.User.Id == default)
            {
                issue.User.FillAuditValuesForCreation();
            }

            await _userRepository.PersistAsync(issue.User);

            if (issue.Repository.Owner.Id == default)
            {
                issue.Repository.Owner.FillAuditValuesForCreation();
                await _userRepository.PersistAsync(issue.Repository.Owner);
            }

            if (issue.Events.OrderByDescending(x => x.CreatedAt).Last().Sender.Id == default)
            {
                issue.Repository.Owner.FillAuditValuesForCreation();
                await _userRepository.PersistAsync(issue.Repository.Owner);
            }

            foreach (var item in issue.Events)
            {
                item.FillAuditValuesForCreation();
            }

            await _issueRepository.PersistAsync(issue, cancellationToken);

            return (issue, validation);
        }

        private async Task<ValidationResult> UpdateRepositoryAsync(Issue issue, CancellationToken cancellationToken = default)
        {
            var validation = new ValidationResult();

            var repository = await _repositoriesRepository.GetByExternalIdAsync(issue.Repository.ExternalId, cancellationToken);

            if (repository != null)
            {
                validation = repository.UpdateValues(issue.Repository);

                if (!validation.IsValid)
                {
                    return validation;
                }

                issue.Repository = repository;
            }
            else
            {
                issue.Repository.FillAuditValuesForCreation();
            }

            return validation;
        }

        private async Task<ValidationResult> UpdateIssueAsync(Issue issue, EventViewModel @event, Dictionary<long, User> userDictionary, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Updating existing issue fields.");

            var updateIssue = _mapper.Map<Issue>(@event);

            issue.UpdateValues(updateIssue);

            issue.FillAuditValuesForUpdate();

            _logger.LogDebug("Creating event representation.");
            var issueEvent = _mapper.Map<IssueEvent>(@event).FillAuditValuesForCreation();
            issueEvent.Issue = issue;

            issue.Events.Add(issueEvent);

            var validation = UpdateRelatedUsers(updateIssue, userDictionary);

            if (!validation.IsValid)
            {
                _logger.LogWarning("Error while trying to update issue data.");
            }
            else
            {
                StandardizeCommonUsers(issue);
            }

            validation = await UpdateRepositoryAsync(issue, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning($"Repository update failed: \n {validation}");
                return validation;
            }

            return validation;
        }

        private void StandardizeCommonUsers(Issue issue)
        {
            var issueEvent = issue.Events.OrderBy(x => x.CreatedAt).Last();

            var currentUsers = new[]
            {
                issue.User,
                issue.Repository.Owner,
                issueEvent.Sender
            }.GroupBy(x => x.ExternalId)
            .ToDictionary(x => x.Key, x => x.First());

            SetUpDistinctUsers(issue.User, currentUsers, user => issue.User = user);
            SetUpDistinctUsers(issue.Repository.Owner, currentUsers, user => issue.Repository.Owner = user);
            SetUpDistinctUsers(issueEvent.Sender, currentUsers, user => issueEvent.Sender = user);
        }

        private ValidationResult UpdateRelatedUsers(Issue issue, Dictionary<long, User> userDictionary)
        {
            var validation = UpdateUserValues(issue.User.ExternalId, userDictionary, issue.User, user =>
            {
                issue.User = user;
            });

            if (!validation.IsValid)
            {
                _logger.LogDebug("Updating issue user failed");
                return validation;
            }

            validation = UpdateUserValues(issue.Repository.Owner.ExternalId, userDictionary, issue.Repository.Owner, user =>
            {
                issue.Repository.Owner = user;
            });

            if (!validation.IsValid)
            {
                _logger.LogDebug("Updating repository owner failed");
                return validation;
            }

            var issueEvent = issue.Events.First();
            validation = UpdateUserValues(issueEvent.Sender.ExternalId, userDictionary, issueEvent.Sender, user =>
            {
                issueEvent.Sender = user;
            });

            if (!validation.IsValid)
            {
                _logger.LogDebug("Issue event user failed");
                return validation;
            }

            return validation;
        }

        private void SetUpDistinctUsers(User user, Dictionary<long, User> userDictionary, Action<User> callback)
            => callback(userDictionary[user.ExternalId]);

        private ValidationResult UpdateUserValues(long userExternalId, Dictionary<long, User> userDictionary, User update, Action<User> callback)
        {
            var validationResult = new ValidationResult();

            if (!userDictionary.ContainsKey(userExternalId))
            {
                return validationResult;
            }

            var existingUser = userDictionary[userExternalId];

            validationResult = existingUser.UpdateValues(update);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            callback(existingUser);

            return validationResult;
        }
    }
}
