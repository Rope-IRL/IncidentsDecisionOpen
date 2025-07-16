using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.EmployeeLogin;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class EmployeeLoginRepository(IncidentDbContext dbContext) : IEmployeeLoginRepository
{
    public async Task<IEnumerable<EmployeeLogin>> GetEmployeeLogins(CancellationToken cancellationToken)
    {
        var employeeLogins = await dbContext.EmployeeLogins.ToListAsync(cancellationToken);

        return employeeLogins;
    }

    public async Task<Result<EmployeeLogin>> GetEmployeeLoginById(int id, CancellationToken cancellationToken)
    {
        var employeeLogin = await dbContext.EmployeeLogins.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (employeeLogin == null)
        {
            return Result<EmployeeLogin>.Failure($"Failed to get EmployeeLogin with id {id}");
        }
        return Result<EmployeeLogin>.Success(employeeLogin);
    }

    public async Task<Result<EmployeeLogin>> CreateEmployeeLogin(EmployeeLogin employeeLogin, CancellationToken cancellationToken)
    {
        await dbContext.EmployeeLogins.AddAsync(employeeLogin, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<EmployeeLogin>.Failure($"Failed to create EmployeeLogin with such parameters {employeeLogin.ToString()}");
        }
        return Result<EmployeeLogin>.Success(employeeLogin);
    }

    public async Task<Result<EmployeeLogin>> UpdateEmployeeLogin(EmployeeLogin employeeLogin, CancellationToken cancellationToken)
    {
        var oldEmployeeLogin = await dbContext.EmployeeLogins.FirstOrDefaultAsync(e => e.Id == employeeLogin.Id, cancellationToken);

        if (oldEmployeeLogin == null)
        {
            return Result<EmployeeLogin>.Failure("Failed to update such employee login");
        }

        oldEmployeeLogin.UpdateLogin(employeeLogin.Login);
        oldEmployeeLogin.UpdateHashedPassword(employeeLogin.HashedPassword);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<EmployeeLogin>.Failure($"Failed to update EmployeeLogin with such parameters {employeeLogin.ToString()}");
        }

        return Result<EmployeeLogin>.Success(employeeLogin);
    }

    public async Task<Result<EmployeeLogin>> DeleteEmployeeLogin(int id, CancellationToken cancellationToken)
    {
        var EmployeeLogin = await dbContext.EmployeeLogins.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (EmployeeLogin == null)
        {
            return Result<EmployeeLogin>.Failure($"Failed to get EmployeeLogin with id {id}");
        }

        dbContext.EmployeeLogins.Remove(EmployeeLogin);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<EmployeeLogin>.Success(EmployeeLogin);
    }

    public async Task<Result<EmployeeLogin>> GetEmployeeByLogin(string login, CancellationToken cancellationToken)
    {
        var employeeLogin = await dbContext.EmployeeLogins.FirstOrDefaultAsync(e => e.Login == login, cancellationToken);

        if (employeeLogin == null)
        {
            return Result<EmployeeLogin>.Failure("Failed to find employee with such login");
        }

        return Result<EmployeeLogin>.Success(employeeLogin);
    }
}