using Microsoft.EntityFrameworkCore;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Infrastructure.Data.Repositories
{
    public class IssueRepository : ExternalEntityBaseRepository<Issue>, IIssueRepository
    {
        public IssueRepository(OctoEventsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Issue?> GetCompleteByExternalIdAsync(long externalId, CancellationToken cancellationToken = default)
            => await _dbContext.Issues
            .Include(x => x.User)
            .Include(x => x.Repository).ThenInclude(x => x.Owner)
            .Include(x => x.Events).ThenInclude(x => x.Sender)
            .SingleOrDefaultAsync(x => x.ExternalId == externalId);

        public override async Task<Issue?> GetByExternalIdAsync(long externalId, CancellationToken cancellationToken = default)
            => await _dbContext.Issues
            .Include(x => x.User)
            .Include(x => x.Repository).ThenInclude(x => x.Owner)
            .SingleOrDefaultAsync(x => x.ExternalId == externalId);
    }
}
