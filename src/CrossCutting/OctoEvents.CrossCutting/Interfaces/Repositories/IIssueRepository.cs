using OctoEvents.Domain.Entities;

namespace OctoEvents.CrossCutting.Interfaces.Repositories
{
    public interface IIssueRepository : IExternalEntityRepository<Issue>
    {
        Task<Issue?> GetCompleteByExternalIdAsync(long externalId, CancellationToken cancellationToken = default);
    }
}
