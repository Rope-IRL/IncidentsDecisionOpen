using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeePosition;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class EmployeePositionRepository(IncidentDbContext dbContext): IEmployeePositionRepository
{
    public async Task<IEnumerable<EmployeePosition>> GetEmployeePositions(CancellationToken cancellationToken)
    {
        var employeePositions = await dbContext.EmployeePositions.ToListAsync(cancellationToken);

        return employeePositions;
    }

    public async Task<Result<EmployeePosition>> GetEmployeePositionById(int id, CancellationToken cancellationToken)
    {
        var employeePosition = await dbContext.EmployeePositions.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (employeePosition == null)
        {
            return Result<EmployeePosition>.Failure($"Failed to get EmployeePosition with id {id}");
        }
        return Result<EmployeePosition>.Success(employeePosition);
    }

    public async Task<Result<EmployeePosition>> CreateEmployeePosition(EmployeePosition employeePosition, CancellationToken cancellationToken)
    {
        await dbContext.EmployeePositions.AddAsync(employeePosition, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<EmployeePosition>.Failure($"Failed to create EmployeePosition with such parameters {employeePosition.ToString()}");
        }
        return Result<EmployeePosition>.Success(employeePosition);
    }

    public async Task<Result<EmployeePosition>> UpdateEmployeePosition(EmployeePosition employeePosition, CancellationToken cancellationToken)
    {
        var oldEmployeePosition = await dbContext.EmployeePositions.FirstOrDefaultAsync(e => e.Id == employeePosition.Id, cancellationToken);

        if (oldEmployeePosition == null)
        {
            return Result<EmployeePosition>.Failure("Failed to find such employee position");
        }

        oldEmployeePosition.UpdateEmployeeId(employeePosition.EmployeeId);
        oldEmployeePosition.UpdatePositionId(employeePosition.PositionId);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<EmployeePosition>.Failure($"Failed to update EmployeePosition with such parameters {employeePosition.ToString()}");
        }

        return Result<EmployeePosition>.Success(employeePosition);
    }

    public async Task<Result<EmployeePosition>> DeleteEmployeePosition(int id, CancellationToken cancellationToken)
    {
        var employeePosition = await dbContext.EmployeePositions.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (employeePosition == null)
        {
            return Result<EmployeePosition>.Failure($"Failed to get EmployeePosition with id {id}");
        }

        dbContext.EmployeePositions.Remove(employeePosition);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<EmployeePosition>.Success(employeePosition);
    }
}