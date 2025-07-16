using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class TechSupportLoginRepository(IncidentDbContext dbContext) : ITechSupportLoginRepository
{
    public async Task<IEnumerable<TechSupportLogin>> GetTechSupportLogins(CancellationToken cancellationToken)
    {
        var techSupportLogins = await dbContext.TechSupportLogins.ToListAsync(cancellationToken);

        return techSupportLogins;
    }

    public async Task<Result<TechSupportLogin>> GetTechSupportLoginById(int id, CancellationToken cancellationToken)
    {
        var techSupportLogin = await dbContext.TechSupportLogins.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (techSupportLogin == null)
        {
            return Result<TechSupportLogin>.Failure($"Failed to get TechSupportLogin with id {id}");
        }
        return Result<TechSupportLogin>.Success(techSupportLogin);
    }

    public async Task<Result<TechSupportLogin>> CreateTechSupportLogin(TechSupportLogin techSupportLogin, CancellationToken cancellationToken)
    {
        await dbContext.TechSupportLogins.AddAsync(techSupportLogin, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<TechSupportLogin>.Failure($"Failed to create TechSupportLogin with such parameters {techSupportLogin.ToString()}");
        }
        return Result<TechSupportLogin>.Success(techSupportLogin);
    }

    public async Task<Result<TechSupportLogin>> UpdateTechSupportLogin(TechSupportLogin techSupportLogin, CancellationToken cancellationToken)
    {
        var oldTechSupportLogin = await dbContext.TechSupportLogins.FirstOrDefaultAsync(e => e.Id == techSupportLogin.Id, cancellationToken);

        if (oldTechSupportLogin == null)
        {
            return Result<TechSupportLogin>.Failure("Failed to find such tech support login");
        }

        oldTechSupportLogin.UpdateLogin(techSupportLogin.Login);
        oldTechSupportLogin.UpdateHashedPassword(techSupportLogin.HashedPassword);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<TechSupportLogin>.Failure($"Failed to update TechSupportLogin with such parameters {techSupportLogin.ToString()}");
        }

        return Result<TechSupportLogin>.Success(techSupportLogin);
    }

    public async Task<Result<TechSupportLogin>> DeleteTechSupportLogin(int id, CancellationToken cancellationToken)
    {
        var techSupportLogin = await dbContext.TechSupportLogins.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (techSupportLogin == null)
        {
            return Result<TechSupportLogin>.Failure($"Failed to get TechSupportLogin with id {id}");
        }

        dbContext.TechSupportLogins.Remove(techSupportLogin);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<TechSupportLogin>.Success(techSupportLogin);
    }

    public async Task<Result<TechSupportLogin>> GetTechSupportLoginByLogin(string login, CancellationToken cancellationToken)
    {
        var techSupportLogin = await dbContext.TechSupportLogins.FirstOrDefaultAsync(e => e.Login == login, cancellationToken);

        if (techSupportLogin == null)
        {
            return Result<TechSupportLogin>.Failure("Failed to find tech support login with such credentials");
        }

        return Result<TechSupportLogin>.Success(techSupportLogin);
    }
}