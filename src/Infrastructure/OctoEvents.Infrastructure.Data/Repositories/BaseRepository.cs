using OctoEvents.Domain.Entities;

namespace OctoEvents.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<T> : IDisposable
        where T : BaseEntity
    {
        private bool _disposed;
        protected readonly OctoEventsDbContext _dbContext;

        public BaseRepository(
            OctoEventsDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task<T> PersistAsync(T entity, CancellationToken cancellationToken = default)
        {
            if(entity.Id == default)
            {
                await _dbContext.AddAsync(entity, cancellationToken);
            }
            else
            {
                _dbContext.Update(entity);
            }

            return entity;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.FindAsync<T>(id, cancellationToken);

        #region Dispose Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _dbContext.Dispose();

            _disposed = true;
        }

        #endregion
    }
}
