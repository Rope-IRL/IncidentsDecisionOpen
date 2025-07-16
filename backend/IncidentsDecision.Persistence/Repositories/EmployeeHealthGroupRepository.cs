using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class EmployeeHealthGroupRepository(IncidentDbContext dbContext): IEmployeeHealthGroupRepository
{
    public async Task<IEnumerable<EmployeeHealthGroup>> GetEmployeeHealthGroups(CancellationToken cancellationToken)
    {
        var employeeHealthGroups = await dbContext.EmployeeHealthGroups.ToListAsync(cancellationToken);

        return employeeHealthGroups;
    }

    public async Task<Result<EmployeeHealthGroup>> GetEmployeeHealthGroupById(int id, CancellationToken cancellationToken)
    {
        var employeeHealthGroup = await dbContext.EmployeeHealthGroups.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (employeeHealthGroup == null)
        {
            return Result<EmployeeHealthGroup>.Failure($"Failed to get EmployeeHealthGroup with id {id}");
        }
        return Result<EmployeeHealthGroup>.Success(employeeHealthGroup);
    }

    public async Task<Result<EmployeeHealthGroup>> CreateEmployeeHealthGroup(EmployeeHealthGroup employeeHealthGroup, CancellationToken cancellationToken)
    {
        await dbContext.EmployeeHealthGroups.AddAsync(employeeHealthGroup, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<EmployeeHealthGroup>.Failure($"Failed to create EmployeeHealthGroup with such parameters {employeeHealthGroup.ToString()}");
        }
        return Result<EmployeeHealthGroup>.Success(employeeHealthGroup);
    }

    public async Task<Result<EmployeeHealthGroup>> UpdateEmployeeHealthGroup(EmployeeHealthGroup employeeHealthGroup, CancellationToken cancellationToken)
    {
        var oldEmployeeHealthGroup = await dbContext.EmployeeHealthGroups.FirstOrDefaultAsync(e => e.Id == employeeHealthGroup.Id, cancellationToken);

        if (oldEmployeeHealthGroup == null)
        {
            return Result<EmployeeHealthGroup>.Failure("Failed to find such employee health group");
        }

        oldEmployeeHealthGroup.UpdateEmployeeId(employeeHealthGroup.EmployeeId);
        oldEmployeeHealthGroup.UpdateHealthGroupId(employeeHealthGroup.HealthGroupId);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<EmployeeHealthGroup>.Failure($"Failed to update EmployeeHealthGroup with such parameters {employeeHealthGroup.ToString()}");
        }

        return Result<EmployeeHealthGroup>.Success(employeeHealthGroup);
    }

    public async Task<Result<EmployeeHealthGroup>> DeleteEmployeeHealthGroup(int id, CancellationToken cancellationToken)
    {
        var employeeHealthGroup = await dbContext.EmployeeHealthGroups.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (employeeHealthGroup == null)
        {
            return Result<EmployeeHealthGroup>.Failure($"Failed to get EmployeeHealthGroup with id {id}");
        }

        dbContext.EmployeeHealthGroups.Remove(employeeHealthGroup);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<EmployeeHealthGroup>.Success(employeeHealthGroup);
    }
}