using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Position;

namespace IncidentsDecision.Core.Repositories;

public interface IPositionRepository
{
    public Task<IEnumerable<Position>> GetPositions(CancellationToken cancellationToken);
    public Task<Result<Position>> GetPositionById(int id, CancellationToken cancellationToken);
    public Task<Result<Position>> CreatePosition(Position position, CancellationToken cancellationToken);
    public Task<Result<Position>> UpdatePosition(Position position, CancellationToken cancellationToken);
    public Task<Result<Position>> DeletePosition(int id, CancellationToken cancellationToken);
}