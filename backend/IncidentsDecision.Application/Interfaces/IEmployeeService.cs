using IncidentsDecision.Application.DTO.EmployeeDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;

namespace IncidentsDecision.Application.Interfaces;

public interface IEmployeeService
{
    public Task<IEnumerable<EmployeeDto>> GetEmployees(CancellationToken cancellationToken);
    public Task<Result<EmployeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeeDto>> CreateEmployee(EmployeeCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeeDto>> UpdateEmployee(EmployeeUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<Employee>> DeleteEmployee(int id, CancellationToken cancellationToken);
}