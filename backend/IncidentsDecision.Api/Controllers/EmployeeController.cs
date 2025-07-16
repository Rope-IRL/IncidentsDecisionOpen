using IncidentsDecision.Application.DTO.EmployeeDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[Authorize(Roles ="Employee")]
[ApiController]
[Route("/api/[controller]/")]
public class EmployeeController(IEmployeeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees(CancellationToken cancellationToken)
    {
        var employees = await service.GetEmployees(cancellationToken);

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employeeResult = await service.GetEmployeeById(id, cancellationToken);
        if (employeeResult.IsSuccess == false)
        {
            return NotFound(employeeResult.Error);
        }

        return Ok(employeeResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromBody] EmployeeUpdateDto dto, CancellationToken cancellationToken)
    {
        var employeeResult = await service.UpdateEmployee(dto, cancellationToken);

        if (employeeResult.IsSuccess == false)
        {
            return BadRequest(employeeResult.Error);
        }

        return Ok(employeeResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeCreateDto dto, CancellationToken cancellationToken)
    {
        var employeeResult = await service.CreateEmployee(dto, cancellationToken);

        if (employeeResult.IsSuccess == false)
        {
            return BadRequest(employeeResult.Error);
        }

        return Ok(employeeResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        var employeeResult = await service.DeleteEmployee(id, cancellationToken);

        if (employeeResult.IsSuccess == false)
        {
            return NotFound(employeeResult.Error);
        }

        return NoContent();
    }

    [HttpGet("info")]
    public async Task<ActionResult> GetEmployeeByIdFromToken(CancellationToken cancellationToken)
    {

        var rawEmployeeId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

        if (rawEmployeeId == null)
        {
            return NotFound("Can't find such employee");
        }

        int employeeId = int.Parse(rawEmployeeId.Value);

        var employeeResult = await service.GetEmployeeById(employeeId, cancellationToken);

        if (employeeResult.IsSuccess == false)
        {
            return NotFound(employeeResult.Error);
        }

        return Ok(employeeResult.Value);
    }
}