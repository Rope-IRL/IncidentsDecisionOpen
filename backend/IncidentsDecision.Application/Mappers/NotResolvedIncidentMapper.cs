using IncidentsDecision.Application.DTO.NotResolvedIncidentsDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.NotResolvedIncident;

namespace IncidentsDecision.Application.Mappers;

public class NotResolvedIncidentMapper
{
    public static Result<NotResolvedIncident> FromCreateDtoToDomain(NotResolvedIncidentCreateDto dto)
    {
        int? id = null;
        var notResolvedIncidentResult = NotResolvedIncident.Create(id, dto.Name, dto.Description);

        if (notResolvedIncidentResult.IsSuccess == false)
        {
            return Result<NotResolvedIncident>.Failure(notResolvedIncidentResult.Error);
        }

        return Result<NotResolvedIncident>.Success(notResolvedIncidentResult.Value);
    }
    public static Result<NotResolvedIncident> FromUpdateDtoToDomain(NotResolvedIncidentUpdateDto dto)
    {
        var notResolvedIncidentResult = NotResolvedIncident.Create(dto.Id, dto.Name, dto.Description, dto.Day,
            dto.Month, dto.Year, dto.Hour, dto.Minutes);

        if (notResolvedIncidentResult.IsSuccess == false)
        {
            return Result<NotResolvedIncident>.Failure(notResolvedIncidentResult.Error);
        }

        return Result<NotResolvedIncident>.Success(notResolvedIncidentResult.Value);
    }

    public static NotResolvedIncidentDto FromDomainToDto(NotResolvedIncident notResolvedIncident)
    {
        var notResolvedIncidentDto = new NotResolvedIncidentDto()
        {
            Id = (int)notResolvedIncident.Id,
            Name = notResolvedIncident.Name,
            Description = notResolvedIncident.Description,
            CreatedAt = notResolvedIncident.CreatedAt
        };

        return notResolvedIncidentDto;
    }
}