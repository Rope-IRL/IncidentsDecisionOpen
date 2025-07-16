using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.NotResolvedIncident;

public interface INotResolvedIncidentRepository
{
    public Task<IEnumerable<NotResolvedIncident>> GetNotResolvedIncidents(CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncident>> GetNotResolvedIncidentById(int id, CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncident>> CreateNotResolvedIncident(NotResolvedIncident NotResolvedIncident, CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncident>> UpdateNotResolvedIncident(NotResolvedIncident NotResolvedIncident, CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncident>> DeleteNotResolvedIncident(int id, CancellationToken cancellationToken);
}