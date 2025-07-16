using IncidentsDecision.Application.DTO.EmployeeDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;

namespace IncidentsDecision.Application.Mappers;

public class EmployeeMapper
{
    public static Result<Employee> FromCreateDtoToDomain(EmployeeCreateDto dto)
    {
        int? id = null;
        int? loginId = null;
        var employeeResult = Employee.Create(id, dto.Name, dto.Surname, dto.Telephone, dto.Gender, loginId);

        if (employeeResult.IsSuccess == false)
        {
            return Result<Employee>.Failure(employeeResult.Error);
        }

        return Result<Employee>.Success(employeeResult.Value);
    }
    public static Result<Employee> FromUpdateDtoToDomain(EmployeeUpdateDto dto)
    {
        int? loginId = null;
        var employeeResult = Employee.Create(dto.Id, dto.Name, dto.Surname, dto.Telephone, dto.Gender, loginId);

        if (employeeResult.IsSuccess == false)
        {
            return Result<Employee>.Failure(employeeResult.Error);
        }

        return Result<Employee>.Success(employeeResult.Value);
    }

    public static EmployeeDto FromDomainToDto(Employee employee)
    {
        var employeeDto = new EmployeeDto()
        {
            Id = (int)employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Telephone = employee.Telephone.Number,
            Gender = employee.Gender.Value
        };

        return employeeDto;
    }
}