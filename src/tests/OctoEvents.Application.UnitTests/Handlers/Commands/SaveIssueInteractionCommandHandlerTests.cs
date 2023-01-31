using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using OctoEvents.Application.Handlers.Commands;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.CrossCutting.IoC.DI;
using OctoEvents.Domain.Entities;
using OctoEvents.Domain.Enum;
using OctoEvents.Domain.Operations.Commands;
using OctoEvents.Domain.ViewModel;
using OctoEvents.Tests.Configuration;
using OctoEvents.Tests.Configuration.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Application.UnitTests.Handlers.Commands
{
    public class SaveIssueInteractionCommandHandlerTests : BaseTestFixture, IClassFixture<DependencyInjectionFixture>
    {
        private static readonly Action<IServiceCollection> _action = (services) =>
        {
            services.AddScoped(x => new Mock<IUnitOfWork>());
            services.AddScoped(x => new Mock<IIssueRepository>());
            services.AddScoped(x => new Mock<IRepositoriesRepository>());
            services.AddScoped(x => new Mock<IUserRepository>());


            services.AddScoped(x => x.GetRequiredService<Mock<IRepositoriesRepository>>().Object);
            services.AddScoped(x => x.GetRequiredService<Mock<IUserRepository>>().Object);
            services.AddScoped(x => x.GetRequiredService<Mock<IUnitOfWork>>().Object);
            services.AddScoped(x => x.GetRequiredService<Mock<IIssueRepository>>().Object);
        };

        private readonly SaveIssueInteractionCommandHandler _handler;

        private const string defaultUrl = "http:localhost";

        public SaveIssueInteractionCommandHandlerTests(DependencyInjectionFixture fixture) : base(fixture, _action)
        {
            _handler = new SaveIssueInteractionCommandHandler(
                _provider.GetRequiredService<IUnitOfWork>(),
                _provider.GetRequiredService<IValidator<EventViewModel>>(),
                _provider.GetRequiredService<IMapper>(),
                _provider.GetRequiredService<IIssueRepository>(),
                _provider.GetRequiredService<IRepositoriesRepository>(),
                _provider.GetRequiredService<IUserRepository>(),
                _provider.GetRequiredService<ILogger<SaveIssueInteractionCommandHandler>>()
            );
        }

        [Fact(DisplayName = "It should reject an invalid event")]
        public async Task ItShouldRejectInvalidEvent()
        {
            var result = await _handler.Handle(new SaveIssueInteractionCommand(new EventViewModel { }), _cancellationTokenSource.Token);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(4);
        }

        [Fact(DisplayName = "It should handle an error")]
        public async Task ItShouldHandleAnError()
        {
            _provider.GetRequiredService<Mock<IIssueRepository>>()
                .Setup(x => x.GetByExternalIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("GENERIC FAILURE EXCEPTION"));

            var unitOfWork = _provider.GetRequiredService<Mock<IUnitOfWork>>();
            unitOfWork.Setup(x => x.IsCompleted).Returns(true);

            var result = await _handler.Handle(new SaveIssueInteractionCommand(GetValidPayload()), _cancellationTokenSource.Token);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(x => x.ErrorMessage == "Error while trying to save issue event.");

            unitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once());
            unitOfWork.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact(DisplayName = "It should create new event")]
        public async Task ItShouldCreateEventSuccessfully()
        {
            var userRepository = _provider.GetRequiredService<Mock<IUserRepository>>();

            userRepository.Setup(x => x.GetAllByExternalIdsAsync(It.IsAny<List<long>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Dictionary<long, Domain.Entities.User>()));

            var user = new User();

            userRepository.Setup(x => x.PersistAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback<User, CancellationToken>((x, c) =>
            {
                x.Id = new("36b18a2e-cb9e-4ab7-9445-b4fea51841a2");
                user = x;
            }).Returns(Task.FromResult(user));

            var unitOfWork = _provider.GetRequiredService<Mock<IUnitOfWork>>();
            unitOfWork.Setup(x => x.IsCompleted).Returns(true);

            var result = await _handler.Handle(new(GetValidPayload()), _cancellationTokenSource.Token);

            result.IsValid.Should().BeTrue();

            unitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once());
            unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            unitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact(DisplayName = "It should update event")]
        public async Task ItShouldUpdateEventSuccessfullly()
        {
            var userRepository = _provider.GetRequiredService<Mock<IUserRepository>>();

            userRepository.Setup(x => x.GetAllByExternalIdsAsync(It.IsAny<List<long>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Dictionary<long, Domain.Entities.User>()));

            var issueRepositoryMock = _provider.GetRequiredService<Mock<IIssueRepository>>();

            issueRepositoryMock.Setup(x => x.GetByExternalIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetExistingIssue()));

            var userList = new[] { GetExampleIssueCreator(), GetExampleIssueContributor() };

            userRepository.Setup(x => x.GetAllByExternalIdsAsync(It.IsAny<List<long>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userList.ToDictionary(x => x.ExternalId)));

            var unitOfWork = _provider.GetRequiredService<Mock<IUnitOfWork>>();
            unitOfWork.Setup(x => x.IsCompleted).Returns(true);

            var result = await _handler.Handle(new(GetValidPayload()), _cancellationTokenSource.Token);

            result.IsValid.Should().BeTrue();

            unitOfWork.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once());
            unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            unitOfWork.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once());

            userList.Should().AllSatisfy(x => x.Type.Should().Be("admin"));
        }

        private User GetExampleIssueCreator()
            =>
            new User
            {
                Id = new Guid("942ede3e-cf59-4fe6-8c9c-9386eadd844f"),
                ExternalId = 3214124,
                Login = "ISSUE_CREATOR_LOGIN",
                NodeId = "dsua8-D0M9S-jd09M",
                Type = "user"
            };

        private User GetExampleIssueContributor()
            =>
            new User
            {
                Id = new Guid("942ede3e-cf59-4fe6-8c9c-9386eadd844f"),
                ExternalId = 4214122,
                Login = "ISSUE_CONTRIBUTOR_LOGIN",
                NodeId = "dsua8-D0M9S-jd09M",
                Type = "user"
            };

        private Issue? GetExistingIssue()
        {
            var issue = new Issue
            {
                Title = "ISSUE TITLE",
                Body = "ISSUE BODY",
                Repository = new()
                {
                    Name = "TEST REPOSITORY",
                    FullName = "TEST REPOSITORY FULL NAME",
                    Owner = GetExampleIssueCreator()
                },
                Events = new()
                {
                    new()
                    {
                         Action = "opened",
                         CreatedAt = new DateTime(2023, 01, 29),
                         CreatedBy = EOperationPerformer.OCTO_EVENTS_API.ToString(),
                         Sender = GetExampleIssueContributor()
                    }
                },
                User = GetExampleIssueCreator()
            };

            return issue;
        }

        private EventViewModel GetValidPayload() =>
            new EventViewModel
            {
                Action = "closed",
                Issue = new()
                {
                    ExternalId = 1231,
                    EventsUrl = defaultUrl,
                    HtmlUrl = defaultUrl,
                    NodeId = "s0djsoDJ9m9pdm9ADPA",
                    Url = defaultUrl,
                    Title = "TITLE TEST",
                    Body = "BODY TEST",
                    Number = 23103,
                    CreatedAt = new DateTime(2023, 01, 29),
                    User = new()
                    {
                        ExternalId = 3214124,
                        Url = defaultUrl,
                        HtmlUrl = defaultUrl,
                        NodeId = "Jpod9p´smipodjas9",
                        Login = "ISSUE_CREATOR_LOGIN",
                        RepositoriesUrl = defaultUrl,
                        Type = "admin",
                        CreatedAt = new DateTime(2023, 01, 29),
                        EventsUrl = defaultUrl
                    }
                },
                Repository = new()
                {
                    ExternalId = 123213,
                    EventsUrl = defaultUrl,
                    CreatedAt = new DateTime(2023, 01, 29),
                    HtmlUrl = defaultUrl,
                    Url = defaultUrl,
                    FullName = "REPOSITORY FULL NAME",
                    Name = "REPOSITORY",
                    NodeId = "8p@!(pmu#j_!)29-32",
                    IssuesUrl = defaultUrl,
                    Owner = new()
                    {
                        ExternalId = 3214124,
                        Url = defaultUrl,
                        HtmlUrl = defaultUrl,
                        NodeId = "Jpod9p´smipodjas9",
                        Login = "ISSUE_CREATOR_LOGIN",
                        RepositoriesUrl = defaultUrl,
                        Type = "admin",
                        CreatedAt = new DateTime(2023, 01, 29),
                        EventsUrl = defaultUrl
                    }
                },
                Sender = new()
                {
                    ExternalId = 4214122,
                    Url = defaultUrl,
                    HtmlUrl = defaultUrl,
                    NodeId = "Jpodda8ho7Ing*7ipodjas9",
                    Login = "ISSUE_CONTRIBUTOR_LOGIN",
                    RepositoriesUrl = defaultUrl,
                    Type = "admin",
                    CreatedAt = new DateTime(2023, 01, 29),
                    EventsUrl = defaultUrl
                }
            };
    }
}
