using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Core.Repositories;

public interface IHealthGroupRepository
{
    public Task<IEnumerable<HealthGroup>> GetHealthGroups(CancellationToken cancellationToken);
    public Task<Result<HealthGroup>> GetHealthGroupById(int id, CancellationToken cancellationToken);
    public Task<Result<HealthGroup>> CreateHealthGroup(HealthGroup healthGroup, CancellationToken cancellationToken);
    public Task<Result<HealthGroup>> UpdateHealthGroup(HealthGroup healthGroup, CancellationToken cancellationToken);
    public Task<Result<HealthGroup>> DeleteHealthGroup(int id, CancellationToken cancellationToken);
}