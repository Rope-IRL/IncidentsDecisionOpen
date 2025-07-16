using IncidentsDecision.Application.DTO.EmployeeLoginDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeeLogin;

namespace IncidentsDecision.Application.Interfaces;

public interface IEmployeeLoginService
{
    public Task<IEnumerable<EmployeeLoginDto>> GetEmployeeLogins(CancellationToken cancellationToken);
    public Task<Result<EmployeeLoginDto>> GetEmployeeLoginById(int id, CancellationToken cancellationToken);
    public Task<Result<EmployeeLoginDto>> CreateEmployeeLogin(EmployeeLoginCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeeLoginDto>> UpdateEmployeeLogin(EmployeeLoginUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<EmployeeLogin>> DeleteEmployeeLogin(int id, CancellationToken cancellationToken);
    public Task<Result<string>> LoginEmployee(string login, string password, CancellationToken cancellationToken);
}