using IncidentsDecision.Application.DTO.ResolvedIncidentDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.ResolvedIncident;

namespace IncidentsDecision.Application.Interfaces;

public interface IResolvedIncidentService
{
    public Task<IEnumerable<ResolvedIncidentDto>> GetResolvedIncidents(CancellationToken cancellationToken);
    public Task<Result<ResolvedIncidentDto>> GetResolvedIncidentById(int id, CancellationToken cancellationToken);
    public Task<Result<ResolvedIncidentDto>> CreateResolvedIncident(ResolvedIncidentCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<ResolvedIncidentDto>> UpdateResolvedIncident(ResolvedIncidentUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<ResolvedIncident>> DeleteResolvedIncident(int id, CancellationToken cancellationToken);
}