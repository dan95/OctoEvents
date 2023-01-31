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
    public abstract class ExternalEntityBaseRepository<T> : BaseRepository<T>, IRepository<T>
        where T : BaseExternalEntity
    {
        protected ExternalEntityBaseRepository(OctoEventsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Dictionary<long, T>> GetAllByExternalIdsAsync(List<long> externalIds, CancellationToken cancellationToken = default)
            => await _dbContext.Set<T>()
            .Where(x => externalIds.Contains(x.ExternalId))
            .ToDictionaryAsync(x => x.ExternalId, x => x, cancellationToken);

        public virtual async Task<T?> GetByExternalIdAsync(long externalId, CancellationToken cancellationToken = default)
            => await _dbContext.Set<T>()
            .SingleOrDefaultAsync(x => x.ExternalId== externalId, cancellationToken);
    }
}
