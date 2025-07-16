using IncidentsDecision.Application.DTO.EmployeeHealthGroupDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;

namespace IncidentsDecision.Application.Interfaces;

public interface IEmployeeHealthGroupService
{
    public Task<IEnumerable<EmployeeHealthGroupDto>> GetEmployeeHealthGroups(CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroupDto>> GetEmployeeHealthGroupById(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroupDto>> CreateEmployeeHealthGroup(EmployeeHealthGroupCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroupDto>> UpdateEmployeeHealthGroup(EmployeeHealthGroupUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroup>> DeleteEmployeeHealthGroup(int id, CancellationToken cancellationToken);
}