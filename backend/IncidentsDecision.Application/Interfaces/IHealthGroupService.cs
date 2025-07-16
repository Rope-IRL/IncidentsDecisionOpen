using IncidentsDecision.Application.DTO.HealthGroupDtos;
using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Application.Interfaces;

public interface IHealthGroupService
{
    public Task<IEnumerable<HealthGroupDto>> GetHealthGroups(CancellationToken cancellationToken);
    public Task<Result<HealthGroupDto>> GetHealthGroupById(int id, CancellationToken cancellationToken);
    public Task<Result<HealthGroupDto>> CreateHealthGroup(HealthGroupCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<HealthGroupDto>> UpdateHealthGroup(HealthGroupUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<HealthGroup>> DeleteHealthGroup(int id, CancellationToken cancellationToken);
}