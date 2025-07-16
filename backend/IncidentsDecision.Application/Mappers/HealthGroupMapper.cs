using IncidentsDecision.Application.DTO.HealthGroupDtos;
using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Application.Mappers;

public class HealthGroupMapper
{
    public static Result<HealthGroup> FromCreateDtoToDomain(HealthGroupCreateDto dto)
    {
        int? id = null;
        var healthGroupResult = HealthGroup.Create(id, dto.Name, dto.Description);

        if (healthGroupResult.IsSuccess == false)
        {
            return Result<HealthGroup>.Failure(healthGroupResult.Error);
        }

        return Result<HealthGroup>.Success(healthGroupResult.Value);
    }
    public static Result<HealthGroup> FromUpdateDtoToDomain(HealthGroupUpdateDto dto)
    {
        var healthGroupResult = HealthGroup.Create(dto.Id, dto.Name, dto.Description);

        if (healthGroupResult.IsSuccess == false)
        {
            return Result<HealthGroup>.Failure(healthGroupResult.Error);
        }

        return Result<HealthGroup>.Success(healthGroupResult.Value);
    }

    public static HealthGroupDto FromDomainToDto(HealthGroup healthGroup)
    {
        var healthGroupDto = new HealthGroupDto()
        {
            Id = (int)healthGroup.Id,
            Name = healthGroup.Name,
            Description = healthGroup.Description 
        };

        return healthGroupDto;
    }
}