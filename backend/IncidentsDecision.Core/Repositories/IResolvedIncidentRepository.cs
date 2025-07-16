using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.ResolvedIncident;

public interface IResolvedIncidentRepository
{
    public Task<IEnumerable<ResolvedIncident>> GetResolvedIncidents(CancellationToken cancellationToken);
    public Task<Result<ResolvedIncident>> GetResolvedIncidentById(int id, CancellationToken cancellationToken);
    public Task<Result<ResolvedIncident>> CreateResolvedIncident(ResolvedIncident ResolvedIncident, CancellationToken cancellationToken);
    public Task<Result<ResolvedIncident>> UpdateResolvedIncident(ResolvedIncident ResolvedIncident, CancellationToken cancellationToken);
    public Task<Result<ResolvedIncident>> DeleteResolvedIncident(int id, CancellationToken cancellationToken);
}