using IncidentsDecision.Application.DTO.EmployeeHealthGroupDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;
using IncidentsDecision.Core.Repositories;

namespace IncidentsDecision.Application.Services;

public class EmployeeHealthGroupService(IEmployeeHealthGroupRepository repo): IEmployeeHealthGroupService 
{
    public async Task<IEnumerable<EmployeeHealthGroupDto>> GetEmployeeHealthGroups(CancellationToken cancellationToken)
    {
        var employeeHealthGroups = await repo.GetEmployeeHealthGroups(cancellationToken);
        var employeeHealthGroupDtos = new List<EmployeeHealthGroupDto>();
        foreach (var EmployeeHealthGroup in employeeHealthGroups)
        {
            employeeHealthGroupDtos.Add(EmployeeHealthGroupMapper.FromDomainToDto(EmployeeHealthGroup));
        }

        return employeeHealthGroupDtos;
    }

    public async Task<Result<EmployeeHealthGroupDto>> GetEmployeeHealthGroupById(int id, CancellationToken cancellationToken)
    {
        var employeeHealthGroupResult = await repo.GetEmployeeHealthGroupById(id, cancellationToken);

        if (employeeHealthGroupResult.IsSuccess == false)
        {
            return Result<EmployeeHealthGroupDto>.Failure(employeeHealthGroupResult.Error);
        }

        var EmployeeHealthGroup = EmployeeHealthGroupMapper.FromDomainToDto(employeeHealthGroupResult.Value);

        return Result<EmployeeHealthGroupDto>.Success(EmployeeHealthGroup);
    }

    public async Task<Result<EmployeeHealthGroupDto>> CreateEmployeeHealthGroup(EmployeeHealthGroupCreateDto dto,
        CancellationToken cancellationToken)
    {
        var employeeHealthGroupResult = EmployeeHealthGroupMapper.FromCreateDtoToDomain(dto);

        if (employeeHealthGroupResult.IsSuccess == false)
        {
            return Result<EmployeeHealthGroupDto>.Failure(employeeHealthGroupResult.Error);
        }

        var createRes = await repo.CreateEmployeeHealthGroup(employeeHealthGroupResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<EmployeeHealthGroupDto>.Failure(createRes.Error);
        }

        var res = Result<EmployeeHealthGroupDto>.Success(EmployeeHealthGroupMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    public async Task<Result<EmployeeHealthGroupDto>> UpdateEmployeeHealthGroup(EmployeeHealthGroupUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var employeeHealthGroupRes = EmployeeHealthGroupMapper.FromUpdateDtoToDomain(dto);

        if (employeeHealthGroupRes.IsSuccess == false)
        {
            return Result<EmployeeHealthGroupDto>.Failure(employeeHealthGroupRes.Error);
        }

        var updateRes = await repo.UpdateEmployeeHealthGroup(employeeHealthGroupRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<EmployeeHealthGroupDto>.Failure(updateRes.Error);
        }

        var res = Result<EmployeeHealthGroupDto>.Success(EmployeeHealthGroupMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<EmployeeHealthGroup>> DeleteEmployeeHealthGroup(int id, CancellationToken cancellationToken)
    {
        var employeeHealthGroupRes = await repo.DeleteEmployeeHealthGroup(id, cancellationToken);

        if (employeeHealthGroupRes.IsSuccess == false)
        {
            return Result<EmployeeHealthGroup>.Failure(employeeHealthGroupRes.Error);
        }

        return Result<EmployeeHealthGroup>.Success(employeeHealthGroupRes.Value);
    }
}
