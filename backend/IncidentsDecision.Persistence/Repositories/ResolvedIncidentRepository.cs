using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.ResolvedIncident;
using Microsoft.EntityFrameworkCore;


public class ResolvedIncidentRepository(IncidentDbContext dbContext): IResolvedIncidentRepository
{
    public async Task<IEnumerable<ResolvedIncident>> GetResolvedIncidents(CancellationToken cancellationToken)
    {
        var ResolvedIncidents = await dbContext.ResolvedIncidents.ToListAsync(cancellationToken);

        return ResolvedIncidents;
    }

    public async Task<Result<ResolvedIncident>> GetResolvedIncidentById(int id, 
        CancellationToken cancellationToken)
    {
        var ResolvedIncident = await dbContext.ResolvedIncidents.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (ResolvedIncident == null)
        {
            return Result<ResolvedIncident>.Failure($"Failed to get ResolvedIncident with id {id}");
        }
        return Result<ResolvedIncident>.Success(ResolvedIncident);
    }

    public async Task<Result<ResolvedIncident>> CreateResolvedIncident(ResolvedIncident ResolvedIncident, 
        CancellationToken cancellationToken)
    {
        await dbContext.ResolvedIncidents.AddAsync(ResolvedIncident, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<ResolvedIncident>.Failure($"Failed to create ResolvedIncident with such parameters {ResolvedIncident.ToString()}");
        }
        return Result<ResolvedIncident>.Success(ResolvedIncident);
    }

    public async Task<Result<ResolvedIncident>> UpdateResolvedIncident(ResolvedIncident ResolvedIncident, 
        CancellationToken cancellationToken)
    {
        var oldResolvedIncident = await dbContext.ResolvedIncidents
            .FirstOrDefaultAsync(e => e.Id == ResolvedIncident.Id, cancellationToken);

        if (oldResolvedIncident == null)
        {
            return Result<ResolvedIncident>.Failure("Failed to update such ResolvedIncident");
        }

        oldResolvedIncident.UpdateName(ResolvedIncident.Name);
        oldResolvedIncident.UpdateDescription(ResolvedIncident.Description);
        oldResolvedIncident.UpdateDateAndTime(ResolvedIncident.CreatedAt);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<ResolvedIncident>.Failure($"Failed to update ResolvedIncident with such parameters {ResolvedIncident.ToString()}");
        }

        return Result<ResolvedIncident>.Success(ResolvedIncident);
    }

    public async Task<Result<ResolvedIncident>> DeleteResolvedIncident(int id, CancellationToken cancellationToken)
    {
        var ResolvedIncident = await dbContext.ResolvedIncidents.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (ResolvedIncident == null)
        {
            return Result<ResolvedIncident>.Failure($"Failed to find such ResolvedIncident");
        }

        dbContext.ResolvedIncidents.Remove(ResolvedIncident);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<ResolvedIncident>.Success(ResolvedIncident);
    }
}

