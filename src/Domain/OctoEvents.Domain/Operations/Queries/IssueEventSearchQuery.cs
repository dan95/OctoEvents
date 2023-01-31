using MediatR;
using OctoEvents.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Domain.Operations.Queries
{
    public class IssueEventSearchQuery : IRequest<List<IssueEventItemViewModel>>
    {
        public long IssueExternalId { get; private set; }

        public IssueEventSearchQuery(long issueExternalId)
        {
            IssueExternalId = issueExternalId;
        }
    }
}
