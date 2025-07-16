using IncidentsDecision.Application.DTO.EmployeeHealthGroupDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;

namespace IncidentsDecision.Application.Mappers;

public class EmployeeHealthGroupMapper
{
    public static Result<EmployeeHealthGroup> FromCreateDtoToDomain(EmployeeHealthGroupCreateDto dto)
    {
        int? id = null;
        var employeeHealthGroupResult = EmployeeHealthGroup.Create(id, dto.EmployeeId, dto.HealthGroupId);

        if (employeeHealthGroupResult.IsSuccess == false)
        {
            return Result<EmployeeHealthGroup>.Failure(employeeHealthGroupResult.Error);
        }

        return Result<EmployeeHealthGroup>.Success(employeeHealthGroupResult.Value);
    }
    public static Result<EmployeeHealthGroup> FromUpdateDtoToDomain(EmployeeHealthGroupUpdateDto dto)
    {
        var employeeHealthGroupResult = EmployeeHealthGroup.Create(dto.Id, dto.EmployeeId, dto.HealthGroupId);

        if (employeeHealthGroupResult.IsSuccess == false)
        {
            return Result<EmployeeHealthGroup>.Failure(employeeHealthGroupResult.Error);
        }

        return Result<EmployeeHealthGroup>.Success(employeeHealthGroupResult.Value);
    }

    public static EmployeeHealthGroupDto FromDomainToDto(EmployeeHealthGroup employeeHealthGroup)
    {
        var employeeHealthGroupDto = new EmployeeHealthGroupDto()
        {
            Id = (int)employeeHealthGroup.Id,
            EmployeeId = employeeHealthGroup.EmployeeId,
            HealthGroupId = employeeHealthGroup.HealthGroupId 
        };

        return employeeHealthGroupDto;
    }
}