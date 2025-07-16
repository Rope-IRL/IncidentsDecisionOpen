using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.NotResolvedIncident;
using Microsoft.EntityFrameworkCore;

public class NotResolvedIncidentRepository(IncidentDbContext dbContext): INotResolvedIncidentRepository
{
    public async Task<IEnumerable<NotResolvedIncident>> GetNotResolvedIncidents(CancellationToken cancellationToken)
    {
        var notResolvedIncidents = await dbContext.NotResolvedIncidents.ToListAsync(cancellationToken);

        return notResolvedIncidents;
    }

    public async Task<Result<NotResolvedIncident>> GetNotResolvedIncidentById(int id, 
        CancellationToken cancellationToken)
    {
        var notResolvedIncident = await dbContext.NotResolvedIncidents.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (notResolvedIncident == null)
        {
            return Result<NotResolvedIncident>.Failure($"Failed to get NotResolvedIncident with id {id}");
        }
        return Result<NotResolvedIncident>.Success(notResolvedIncident);
    }

    public async Task<Result<NotResolvedIncident>> CreateNotResolvedIncident(NotResolvedIncident notResolvedIncident, 
        CancellationToken cancellationToken)
    {
        await dbContext.NotResolvedIncidents.AddAsync(notResolvedIncident, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<NotResolvedIncident>.Failure($"Failed to create NotResolvedIncident with such parameters {notResolvedIncident.ToString()}");
        }
        return Result<NotResolvedIncident>.Success(notResolvedIncident);
    }

    public async Task<Result<NotResolvedIncident>> UpdateNotResolvedIncident(NotResolvedIncident notResolvedIncident, 
        CancellationToken cancellationToken)
    {
        var oldNotResolvedIncident = await dbContext.NotResolvedIncidents
            .FirstOrDefaultAsync(e => e.Id == notResolvedIncident.Id, cancellationToken);

        if (oldNotResolvedIncident == null)
        {
            return Result<NotResolvedIncident>.Failure("Failed to update such NotResolvedIncident");
        }

        Console.WriteLine($"New not resolved Incident is {notResolvedIncident.ToString()}");
        Console.WriteLine($"Old not resolved Incident is {oldNotResolvedIncident.ToString()}");

        oldNotResolvedIncident.UpdateName(notResolvedIncident.Name);
        oldNotResolvedIncident.UpdateDescription(notResolvedIncident.Description);
        // A bug may occur here because previously the number of seconds could be non-zero, 
        // but now seconds are always set to 0 by default.
        // This might be changed in the future, but for now (and maybe even later), seconds don't matter.
        oldNotResolvedIncident.UpdateDateAndTime(notResolvedIncident.CreatedAt);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<NotResolvedIncident>.Failure($"Failed to update NotResolvedIncident with such parameters {notResolvedIncident.ToString()}");
        }

        return Result<NotResolvedIncident>.Success(notResolvedIncident);
    }

    public async Task<Result<NotResolvedIncident>> DeleteNotResolvedIncident(int id, CancellationToken cancellationToken)
    {
        var notResolvedIncident = await dbContext.NotResolvedIncidents.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (notResolvedIncident == null)
        {
            return Result<NotResolvedIncident>.Failure($"Failed to find such NotResolvedIncident");
        }

        dbContext.NotResolvedIncidents.Remove(notResolvedIncident);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<NotResolvedIncident>.Success(notResolvedIncident);
    }
}
