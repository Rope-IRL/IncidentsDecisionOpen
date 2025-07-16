using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.EmployeeLogin;

namespace IncidentsDecision.Core.Repositories;

public interface IEmployeeLoginRepository
{
    public Task<IEnumerable<EmployeeLogin>> GetEmployeeLogins(CancellationToken cancellationToken);
    public Task<Result<EmployeeLogin>> GetEmployeeLoginById(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeeLogin>> CreateEmployeeLogin(EmployeeLogin employeeLogin, CancellationToken cancellationToken);
    public Task<Result<EmployeeLogin>> UpdateEmployeeLogin(EmployeeLogin employeeLogin, CancellationToken cancellationToken);
    public Task<Result<EmployeeLogin>> DeleteEmployeeLogin(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeeLogin>> GetEmployeeByLogin(string login, CancellationToken cancellationToken);
}