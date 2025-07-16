using IncidentsDecision.Application.DTO.EmployeePositionDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeePosition;

namespace IncidentsDecision.Application.Interfaces;

public interface IEmployeePositionService
{
    public Task<IEnumerable<EmployeePositionDto>> GetEmployeePositions(CancellationToken cancellationToken);
    public Task<Result<EmployeePositionDto>> GetEmployeePositionById(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeePositionDto>> CreateEmployeePosition(EmployeePositionCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeePositionDto>> UpdateEmployeePosition(EmployeePositionUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeePosition>> DeleteEmployeePosition(int id, CancellationToken cancellationToken);
}