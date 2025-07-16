using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Core.Repositories;

public interface ITechSupportLoginRepository
{
    public Task<IEnumerable<TechSupportLogin>> GetTechSupportLogins(CancellationToken cancellationToken);
    public Task<Result<TechSupportLogin>> GetTechSupportLoginById(int id, CancellationToken cancellationToken);
    public Task<Result<TechSupportLogin>> CreateTechSupportLogin(TechSupportLogin techSupportLogin, CancellationToken cancellationToken);
    public Task<Result<TechSupportLogin>> UpdateTechSupportLogin(TechSupportLogin techSupportLogin, CancellationToken cancellationToken);
    public Task<Result<TechSupportLogin>> DeleteTechSupportLogin(int id, CancellationToken cancellationToken);
    public Task<Result<TechSupportLogin>> GetTechSupportLoginByLogin(string login, CancellationToken cancellationToken);
}