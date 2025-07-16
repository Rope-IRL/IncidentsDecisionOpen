using IncidentsDecision.Application.DTO.PositionDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Position;
using IncidentsDecision.Core.Repositories;

namespace IncidentsDecision.Application.Services;

public class PositionService(IPositionRepository repo): IPositionService 
{
    public async Task<IEnumerable<PositionDto>> GetPositions(CancellationToken cancellationToken)
    {
        var positions = await repo.GetPositions(cancellationToken);
        var positionDtos = new List<PositionDto>();
        foreach (var position in positions)
        {
            positionDtos.Add(PositionMapper.FromDomainToDto(position));
        }

        return positionDtos;
    }

    public async Task<Result<PositionDto>> GetPositionById(int id, CancellationToken cancellationToken)
    {
        var positionResult = await repo.GetPositionById(id, cancellationToken);

        if (positionResult.IsSuccess == false)
        {
            return Result<PositionDto>.Failure(positionResult.Error);
        }

        var position = PositionMapper.FromDomainToDto(positionResult.Value);

        return Result<PositionDto>.Success(position);
    }

    public async Task<Result<PositionDto>> CreatePosition(PositionCreateDto dto, CancellationToken cancellationToken)
    {
        var positionResult = PositionMapper.FromCreateDtoToDomain(dto);

        if (positionResult.IsSuccess == false)
        {
            return Result<PositionDto>.Failure(positionResult.Error);
        }

        var createRes = await repo.CreatePosition(positionResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<PositionDto>.Failure(createRes.Error);
        }

        var res = Result<PositionDto>.Success(PositionMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    public async Task<Result<PositionDto>> UpdatePosition(PositionUpdateDto dto, CancellationToken cancellationToken)
    {
        var positionRes = PositionMapper.FromUpdateDtoToDomain(dto);

        if (positionRes.IsSuccess == false)
        {
            return Result<PositionDto>.Failure(positionRes.Error);
        }

        var updateRes = await repo.UpdatePosition(positionRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<PositionDto>.Failure(updateRes.Error);
        }

        var res = Result<PositionDto>.Success(PositionMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<Position>> DeletePosition(int id, CancellationToken cancellationToken)
    {
        var positionRes = await repo.DeletePosition(id, cancellationToken);

        if (positionRes.IsSuccess == false)
        {
            return Result<Position>.Failure(positionRes.Error);
        }

        return Result<Position>.Success(positionRes.Value);
    }
}