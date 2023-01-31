using OctoEvents.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.CrossCutting.Interfaces.Repositories
{
    public interface IExternalEntityRepository<T> : IRepository<T>
        where T : BaseExternalEntity
    {
        Task<T?> GetByExternalIdAsync(long externalId, CancellationToken cancellationToken = default);
        Task<Dictionary<long, T>> GetAllByExternalIdsAsync(List<long> externalIds,  CancellationToken cancellationToken = default);
    }
}
