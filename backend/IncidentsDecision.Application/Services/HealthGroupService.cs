using IncidentsDecision.Application.DTO.HealthGroupDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Repositories;

namespace IncidentsDecision.Application.Services;

public class HealthGroupService(IHealthGroupRepository repo): IHealthGroupService 
{
    public async Task<IEnumerable<HealthGroupDto>> GetHealthGroups(CancellationToken cancellationToken)
    {
        var HealthGroups = await repo.GetHealthGroups(cancellationToken);
        var HealthGroupDtos = new List<HealthGroupDto>();
        foreach (var HealthGroup in HealthGroups)
        {
            HealthGroupDtos.Add(HealthGroupMapper.FromDomainToDto(HealthGroup));
        }

        return HealthGroupDtos;
    }

    public async Task<Result<HealthGroupDto>> GetHealthGroupById(int id, CancellationToken cancellationToken)
    {
        var HealthGroupResult = await repo.GetHealthGroupById(id, cancellationToken);

        if (HealthGroupResult.IsSuccess == false)
        {
            return Result<HealthGroupDto>.Failure(HealthGroupResult.Error);
        }

        var HealthGroup = HealthGroupMapper.FromDomainToDto(HealthGroupResult.Value);

        return Result<HealthGroupDto>.Success(HealthGroup);
    }

    public async Task<Result<HealthGroupDto>> CreateHealthGroup(HealthGroupCreateDto dto, CancellationToken cancellationToken)
    {
        var HealthGroupResult = HealthGroupMapper.FromCreateDtoToDomain(dto);

        if (HealthGroupResult.IsSuccess == false)
        {
            return Result<HealthGroupDto>.Failure(HealthGroupResult.Error);
        }

        var createRes = await repo.CreateHealthGroup(HealthGroupResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<HealthGroupDto>.Failure(createRes.Error);
        }

        var res = Result<HealthGroupDto>.Success(HealthGroupMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    public async Task<Result<HealthGroupDto>> UpdateHealthGroup(HealthGroupUpdateDto dto, CancellationToken cancellationToken)
    {
        var HealthGroupRes = HealthGroupMapper.FromUpdateDtoToDomain(dto);

        if (HealthGroupRes.IsSuccess == false)
        {
            return Result<HealthGroupDto>.Failure(HealthGroupRes.Error);
        }

        var updateRes = await repo.UpdateHealthGroup(HealthGroupRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<HealthGroupDto>.Failure(updateRes.Error);
        }

        var res = Result<HealthGroupDto>.Success(HealthGroupMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<HealthGroup>> DeleteHealthGroup(int id, CancellationToken cancellationToken)
    {
        var HealthGroupRes = await repo.DeleteHealthGroup(id, cancellationToken);

        if (HealthGroupRes.IsSuccess == false)
        {
            return Result<HealthGroup>.Failure(HealthGroupRes.Error);
        }

        return Result<HealthGroup>.Success(HealthGroupRes.Value);
    }
}