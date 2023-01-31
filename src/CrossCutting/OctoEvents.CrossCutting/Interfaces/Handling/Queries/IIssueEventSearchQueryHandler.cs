using MediatR;
using OctoEvents.Domain.Operations.Queries;
using OctoEvents.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.CrossCutting.Interfaces.Handling.Queries
{
    public interface IIssueEventSearchQueryHandler : IRequestHandler<IssueEventSearchQuery, List<IssueEventItemViewModel>>
    {
    }
}
