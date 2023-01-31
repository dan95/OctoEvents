using Microsoft.EntityFrameworkCore.Storage;
using OctoEvents.CrossCutting.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly OctoEventsDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public bool IsCompleted { get; private set; }

        public UnitOfWork(
            OctoEventsDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if(_transaction != null)
            {
                return;
            }

            _transaction = _dbContext.Database.CurrentTransaction ?? await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                return;
            }

            await _transaction.CommitAsync(cancellationToken);

            _transaction = null;
            IsCompleted = true;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if(_transaction == null)
            {
                return;
            }

            await _transaction.RollbackAsync(cancellationToken);

            _transaction = null;
            IsCompleted = true;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        #region Dispose implementation;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose( bool disposing )
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
