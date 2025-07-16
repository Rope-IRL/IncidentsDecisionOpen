using IncidentsDecision.Application.DTO.PositionDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Position;

namespace IncidentsDecision.Application.Mappers;

public class PositionMapper
{
    public static Result<Position> FromCreateDtoToDomain(PositionCreateDto dto)
    {
        int? id = null;
        var positionResult = Position.Create(id, dto.Name, dto.Description);

        if (positionResult.IsSuccess == false)
        {
            return Result<Position>.Failure(positionResult.Error);
        }

        return Result<Position>.Success(positionResult.Value);
    }
    public static Result<Position> FromUpdateDtoToDomain(PositionUpdateDto dto)
    {
        var positionResult = Position.Create(dto.Id, dto.Name, dto.Description);

        if (positionResult.IsSuccess == false)
        {
            return Result<Position>.Failure(positionResult.Error);
        }

        return Result<Position>.Success(positionResult.Value);
    }

    public static PositionDto FromDomainToDto(Position Position)
    {
        var PositionDto = new PositionDto()
        {
            Id = (int)Position.Id,
            Name = Position.Name,
            Description = Position.Description 
        };

        return PositionDto;
    }
}