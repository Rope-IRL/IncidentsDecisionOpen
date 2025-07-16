using IncidentsDecision.Application.DTO.NotResolvedIncidentsDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.NotResolvedIncident;

namespace IncidentsDecision.Application.Interfaces;

public interface INotResolvedIncidentService
{
    public Task<IEnumerable<NotResolvedIncidentDto>> GetNotResolvedIncidents(CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncidentDto>> GetNotResolvedIncidentById(int id, CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncidentDto>> CreateNotResolvedIncident(NotResolvedIncidentCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncidentDto>> UpdateNotResolvedIncident(NotResolvedIncidentUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<NotResolvedIncident>> DeleteNotResolvedIncident(int id, CancellationToken cancellationToken);
}