using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class HealthGroupRepository(IncidentDbContext dbContext): IHealthGroupRepository
{
    public async Task<IEnumerable<HealthGroup>> GetHealthGroups(CancellationToken cancellationToken)
    {
        var healthGroups = await dbContext.HealthGroups.ToListAsync(cancellationToken);

        return healthGroups;
    }

    public async Task<Result<HealthGroup>> GetHealthGroupById(int id, CancellationToken cancellationToken)
    {
        var healthGroup = await dbContext.HealthGroups.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (healthGroup == null)
        {
            return Result<HealthGroup>.Failure($"Failed to find such health group");
        }
        return Result<HealthGroup>.Success(healthGroup);
    }

    public async Task<Result<HealthGroup>> CreateHealthGroup(HealthGroup healthGroup, CancellationToken cancellationToken)
    {
        await dbContext.HealthGroups.AddAsync(healthGroup, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<HealthGroup>.Failure($"Failed to create HealthGroup with such parameters {healthGroup.ToString()}");
        }
        return Result<HealthGroup>.Success(healthGroup);
    }

    public async Task<Result<HealthGroup>> UpdateHealthGroup(HealthGroup healthGroup, CancellationToken cancellationToken)
    {
        var oldHealthGroup = await dbContext.HealthGroups.FirstOrDefaultAsync(e => e.Id == healthGroup.Id, cancellationToken);

        oldHealthGroup = healthGroup;

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<HealthGroup>.Failure($"Failed to update HealthGroup with such parameters {healthGroup.ToString()}");
        }

        return Result<HealthGroup>.Success(healthGroup);
    }

    public async Task<Result<HealthGroup>> DeleteHealthGroup(int id, CancellationToken cancellationToken)
    {
        var healthGroup = await dbContext.HealthGroups.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (healthGroup == null)
        {
            return Result<HealthGroup>.Failure($"Failed to find such health group");
        }

        dbContext.HealthGroups.Remove(healthGroup);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<HealthGroup>.Success(healthGroup);
    }
}