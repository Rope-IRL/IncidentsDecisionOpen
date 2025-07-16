using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.TechSupport;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class TechSupportRepository(IncidentDbContext dbContext): ITechSupportRepository
{
    public async Task<IEnumerable<TechSupport>> GetTechSupports(CancellationToken cancellationToken)
    {
        var techSupports = await dbContext.TechSupports.ToListAsync(cancellationToken);

        return techSupports;
    }

    public async Task<Result<TechSupport>> GetTechSupportById(int id, CancellationToken cancellationToken)
    {
        var techSupport = await dbContext.TechSupports.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (techSupport == null)
        {
            return Result<TechSupport>.Failure($"Failed to get TechSupport with id {id}");
        }
        return Result<TechSupport>.Success(techSupport);
    }

    public async Task<Result<TechSupport>> CreateTechSupport(TechSupport techSupport, CancellationToken cancellationToken)
    {
        await dbContext.TechSupports.AddAsync(techSupport, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<TechSupport>.Failure($"Failed to create TechSupport with such parameters {techSupport.ToString()}");
        }
        return Result<TechSupport>.Success(techSupport);
    }

    public async Task<Result<TechSupport>> UpdateTechSupport(TechSupport techSupport, CancellationToken cancellationToken)
    {
        var oldTechSupport = await dbContext.TechSupports.FirstOrDefaultAsync(e => e.Id == techSupport.Id, cancellationToken);

        if (oldTechSupport == null)
        {
            return Result<TechSupport>.Failure("Failed to find such tech support");
        }

        oldTechSupport.UpdateName(techSupport.Name);
        oldTechSupport.UpdateSurname(techSupport.Surname);
        oldTechSupport.UpdateTelephone(techSupport.Telephone);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<TechSupport>.Failure($"Failed to update TechSupport with such parameters {techSupport.ToString()}");
        }

        return Result<TechSupport>.Success(techSupport);
    }

    public async Task<Result<TechSupport>> DeleteTechSupport(int id, CancellationToken cancellationToken)
    {
        var techSupport = await dbContext.TechSupports.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (techSupport == null)
        {
            return Result<TechSupport>.Failure($"Failed to get TechSupport with id {id}");
        }

        dbContext.TechSupports.Remove(techSupport);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<TechSupport>.Success(techSupport);
    }
}