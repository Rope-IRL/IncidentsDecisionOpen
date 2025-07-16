using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository(IncidentDbContext dbContext): IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetEmployees(CancellationToken cancellationToken)
    {
        var employees = await dbContext.Employees.ToListAsync(cancellationToken);

        return employees;
    }

    public async Task<Result<Employee>> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (employee == null)
        {
            return Result<Employee>.Failure($"Failed to get employee with id {id}");
        }
        return Result<Employee>.Success(employee);
    }

    public async Task<Result<Employee>> CreateEmployee(Employee employee, CancellationToken cancellationToken)
    {
        await dbContext.Employees.AddAsync(employee, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<Employee>.Failure($"Failed to create employee with such parameters {employee.ToString()}");
        }
        return Result<Employee>.Success(employee);
    }

    public async Task<Result<Employee>> UpdateEmployee(Employee employee, CancellationToken cancellationToken)
    {
        var oldEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id, cancellationToken);

        if (oldEmployee == null)
        {
            return Result<Employee>.Failure("Failed to update such employee");
        }

        oldEmployee.UpdateName(employee.Name);
        oldEmployee.UpdateSurname(employee.Surname);
        oldEmployee.UpdateGender(employee.Gender);
        oldEmployee.UpdateTelephone(employee.Telephone);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<Employee>.Failure($"Failed to update employee with such parameters {employee.ToString()}");
        }

        return Result<Employee>.Success(employee);
    }

    public async Task<Result<Employee>> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (employee == null)
        {
            return Result<Employee>.Failure($"Failed to find such employee");
        }

        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Employee>.Success(employee);
    }
}