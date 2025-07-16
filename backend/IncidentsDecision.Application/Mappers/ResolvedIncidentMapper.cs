using IncidentsDecision.Application.DTO.ResolvedIncidentDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.ResolvedIncident;

namespace IncidentsDecision.Application.Mappers;

public class ResolvedIncidentMapper
{
    public static Result<ResolvedIncident> FromCreateDtoToDomain(ResolvedIncidentCreateDto dto)
    {
        int? id = null;
        int seconds = 0;
        var ResolvedIncidentResult = ResolvedIncident.Create(id, dto.Name, dto.Description, dto.Day, dto.Month,
            dto.Year, dto.Hour, dto.Minutes, seconds);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return Result<ResolvedIncident>.Failure(ResolvedIncidentResult.Error);
        }

        return Result<ResolvedIncident>.Success(ResolvedIncidentResult.Value);
    }
    public static Result<ResolvedIncident> FromUpdateDtoToDomain(ResolvedIncidentUpdateDto dto)
    {
        int seconds = 0;
        var ResolvedIncidentResult = ResolvedIncident.Create(dto.Id, dto.Name, dto.Description, dto.Day,
            dto.Month, dto.Year, dto.Hour, dto.Minutes, seconds);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return Result<ResolvedIncident>.Failure(ResolvedIncidentResult.Error);
        }

        return Result<ResolvedIncident>.Success(ResolvedIncidentResult.Value);
    }

    public static ResolvedIncidentDto FromDomainToDto(ResolvedIncident ResolvedIncident)
    {
        var ResolvedIncidentDto = new ResolvedIncidentDto()
        {
            Id = (int)ResolvedIncident.Id,
            Name = ResolvedIncident.Name,
            Description = ResolvedIncident.Description,
            CreatedAt = ResolvedIncident.CreatedAt
        };

        return ResolvedIncidentDto;
    }
}