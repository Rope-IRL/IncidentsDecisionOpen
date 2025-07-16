using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;

namespace IncidentsDecision.Core.Repositories;

public interface IEmployeeHealthGroupRepository
{
    public Task<IEnumerable<EmployeeHealthGroup>> GetEmployeeHealthGroups(CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroup>> GetEmployeeHealthGroupById(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroup>> CreateEmployeeHealthGroup(EmployeeHealthGroup employeeHealthGroup, CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroup>> UpdateEmployeeHealthGroup(EmployeeHealthGroup employeeHealthGroup, CancellationToken cancellationToken);
    public Task<Result<EmployeeHealthGroup>> DeleteEmployeeHealthGroup(int id, CancellationToken cancellationToken);

}