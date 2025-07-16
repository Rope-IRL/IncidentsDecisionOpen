using IncidentsDecision.Application.DTO.EmployeeDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace IncidentsDecision.Application.Services;

public class EmployeeService(IEmployeeRepository repo) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetEmployees(CancellationToken cancellationToken)
    {
        var employees = await repo.GetEmployees(cancellationToken);
        var employeeDtos = new List<EmployeeDto>();
        foreach (var employee in employees)
        {
            employeeDtos.Add(EmployeeMapper.FromDomainToDto(employee));
        }

        return employeeDtos;
    }

    public async Task<Result<EmployeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employeeResult = await repo.GetEmployeeById(id, cancellationToken);

        if (employeeResult.IsSuccess == false)
        {
            return Result<EmployeeDto>.Failure(employeeResult.Error);
        }

        var employee = EmployeeMapper.FromDomainToDto(employeeResult.Value);

        return Result<EmployeeDto>.Success(employee);
    }

    public async Task<Result<EmployeeDto>> CreateEmployee(EmployeeCreateDto dto, CancellationToken cancellationToken)
    {
        var employeeResult = EmployeeMapper.FromCreateDtoToDomain(dto);

        if (employeeResult.IsSuccess == false)
        {
            return Result<EmployeeDto>.Failure(employeeResult.Error);
        }

        var createRes = await repo.CreateEmployee(employeeResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<EmployeeDto>.Failure(createRes.Error);
        }

        var res = Result<EmployeeDto>.Success(EmployeeMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    //This can brake update login, because in mapper login id is null
    public async Task<Result<EmployeeDto>> UpdateEmployee(EmployeeUpdateDto dto, CancellationToken cancellationToken)
    {
        var employeeRes = EmployeeMapper.FromUpdateDtoToDomain(dto);

        if (employeeRes.IsSuccess == false)
        {
            return Result<EmployeeDto>.Failure(employeeRes.Error);
        }

        var updateRes = await repo.UpdateEmployee(employeeRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<EmployeeDto>.Failure(updateRes.Error);
        }

        var res = Result<EmployeeDto>.Success(EmployeeMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<Employee>> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        var employeeRes = await repo.DeleteEmployee(id, cancellationToken);

        if (employeeRes.IsSuccess == false)
        {
            return Result<Employee>.Failure(employeeRes.Error);
        }

        return Result<Employee>.Success(employeeRes.Value);
    }
}