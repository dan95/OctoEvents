using OctoEvents.Domain.Entities;

namespace OctoEvents.CrossCutting.Interfaces.Repositories
{
    public interface IRepository <T> where T : BaseEntity
    {
        Task<T> PersistAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
