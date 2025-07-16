using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;

namespace IncidentsDecision.Core.Repositories;

public interface IEmployeeRepository
{
    public Task<IEnumerable<Employee>> GetEmployees(CancellationToken cancellationToken);
    public Task<Result<Employee>> GetEmployeeById(int id, CancellationToken cancellationToken);
    public Task<Result<Employee>> CreateEmployee(Employee employee, CancellationToken cancellationToken);
    public Task<Result<Employee>> UpdateEmployee(Employee employee, CancellationToken cancellationToken);
    public Task<Result<Employee>> DeleteEmployee(int id, CancellationToken cancellationToken);
}