using AutoMapper;
using Microsoft.Extensions.Logging;
using OctoEvents.CrossCutting.Interfaces.Handling.Queries;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.Domain.Operations.Queries;
using OctoEvents.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Application.Handlers.Queries
{
    public class IssueEventSearchQueryHandler : IIssueEventSearchQueryHandler
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueEventSearchQueryHandler> _logger;

        public IssueEventSearchQueryHandler(
            IIssueRepository issueRepository,
            IMapper mapper,
            ILogger<IssueEventSearchQueryHandler> logger
            )
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<IssueEventItemViewModel>> Handle(IssueEventSearchQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Searching for issue {request.IssueExternalId}");
            var issue = await _issueRepository.GetCompleteByExternalIdAsync(request.IssueExternalId);

            if (issue == null)
            {
                _logger.LogInformation($"Issue {request.IssueExternalId} not found");
                return new();
            }

            _logger.LogDebug("Converting to response");
            var response = _mapper.Map<List<IssueEventItemViewModel>>(issue.Events.OrderByDescending(x => x.CreatedAt));

            return response;
        }
    }
}
