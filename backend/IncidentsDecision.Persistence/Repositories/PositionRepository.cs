using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Position;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore;

public class PositionRepository(IncidentDbContext dbContext): IPositionRepository
{
    public async Task<IEnumerable<Position>> GetPositions(CancellationToken cancellationToken)
    {
        var positions = await dbContext.Positions.ToListAsync(cancellationToken);

        return positions;
    }

    public async Task<Result<Position>> GetPositionById(int id, CancellationToken cancellationToken)
    {
        var position = await dbContext.Positions.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (position == null)
        {
            return Result<Position>.Failure($"Failed to get Position with id {id}");
        }
        return Result<Position>.Success(position);
    }

    public async Task<Result<Position>> CreatePosition(Position position, CancellationToken cancellationToken)
    {
        await dbContext.Positions.AddAsync(position, cancellationToken);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        if (res == 0)
        {
            return Result<Position>.Failure($"Failed to create Position with such parameters {position.ToString()}");
        }
        return Result<Position>.Success(position);
    }

    public async Task<Result<Position>> UpdatePosition(Position position, CancellationToken cancellationToken)
    {
        var oldPosition = await dbContext.Positions.FirstOrDefaultAsync(e => e.Id == position.Id, cancellationToken);

        if (oldPosition == null)
        {
            return Result<Position>.Failure("Failed to find such position");
        }

        oldPosition.UpdateName(position.Name);
        oldPosition.UpdateDescription(position.Description);

        int res = await dbContext.SaveChangesAsync(cancellationToken);

        if (res == 0)
        {
            return Result<Position>.Failure($"Failed to update Position with such parameters {position.ToString()}");
        }

        return Result<Position>.Success(position);
    }

    public async Task<Result<Position>> DeletePosition(int id, CancellationToken cancellationToken)
    {
        var position = await dbContext.Positions.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (position == null)
        {
            return Result<Position>.Failure($"Failed to get Position with id {id}");
        }

        dbContext.Positions.Remove(position);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Position>.Success(position);
    }
}