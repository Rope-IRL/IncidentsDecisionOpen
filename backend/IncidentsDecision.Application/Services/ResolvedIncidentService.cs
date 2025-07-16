using IncidentsDecision.Application.DTO.ResolvedIncidentDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.ResolvedIncident;

namespace IncidentsDecision.Application.Services;

public class ResolvedIncidentService(IResolvedIncidentRepository repo): IResolvedIncidentService 
{
    public async Task<IEnumerable<ResolvedIncidentDto>> GetResolvedIncidents(CancellationToken cancellationToken)
    {
        var ResolvedIncidents = await repo.GetResolvedIncidents(cancellationToken);
        var ResolvedIncidentDtos = new List<ResolvedIncidentDto>();
        foreach (var ResolvedIncident in ResolvedIncidents)
        {
            ResolvedIncidentDtos.Add(ResolvedIncidentMapper.FromDomainToDto(ResolvedIncident));
        }

        return ResolvedIncidentDtos;
    }

    public async Task<Result<ResolvedIncidentDto>> GetResolvedIncidentById(int id, CancellationToken cancellationToken)
    {
        var ResolvedIncidentResult = await repo.GetResolvedIncidentById(id, cancellationToken);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return Result<ResolvedIncidentDto>.Failure(ResolvedIncidentResult.Error);
        }

        var ResolvedIncident = ResolvedIncidentMapper.FromDomainToDto(ResolvedIncidentResult.Value);

        return Result<ResolvedIncidentDto>.Success(ResolvedIncident);
    }

    public async Task<Result<ResolvedIncidentDto>> CreateResolvedIncident(ResolvedIncidentCreateDto dto, CancellationToken cancellationToken)
    {
        var ResolvedIncidentResult = ResolvedIncidentMapper.FromCreateDtoToDomain(dto);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return Result<ResolvedIncidentDto>.Failure(ResolvedIncidentResult.Error);
        }

        var createRes = await repo.CreateResolvedIncident(ResolvedIncidentResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<ResolvedIncidentDto>.Failure(createRes.Error);
        }

        var res = Result<ResolvedIncidentDto>.Success(ResolvedIncidentMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    public async Task<Result<ResolvedIncidentDto>> UpdateResolvedIncident(ResolvedIncidentUpdateDto dto, CancellationToken cancellationToken)
    {
        var ResolvedIncidentRes = ResolvedIncidentMapper.FromUpdateDtoToDomain(dto);

        if (ResolvedIncidentRes.IsSuccess == false)
        {
            return Result<ResolvedIncidentDto>.Failure(ResolvedIncidentRes.Error);
        }

        var updateRes = await repo.UpdateResolvedIncident(ResolvedIncidentRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<ResolvedIncidentDto>.Failure(updateRes.Error);
        }

        var res = Result<ResolvedIncidentDto>.Success(ResolvedIncidentMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<ResolvedIncident>> DeleteResolvedIncident(int id, CancellationToken cancellationToken)
    {
        var ResolvedIncidentRes = await repo.DeleteResolvedIncident(id, cancellationToken);

        if (ResolvedIncidentRes.IsSuccess == false)
        {
            return Result<ResolvedIncident>.Failure(ResolvedIncidentRes.Error);
        }

        return Result<ResolvedIncident>.Success(ResolvedIncidentRes.Value);
    }
}