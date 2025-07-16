using IncidentsDecision.Application.DTO.EmployeePositionDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class EmployeePositionController(IEmployeePositionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeePositionDto>>> GetEmployeePositions(CancellationToken cancellationToken)
    {
        var EmployeePositions = await service.GetEmployeePositions(cancellationToken);

        return Ok(EmployeePositions);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeePositionDto>> GetEmployeePositionById(int id, CancellationToken cancellationToken)
    {
        var EmployeePositionResult = await service.GetEmployeePositionById(id, cancellationToken);
        if (EmployeePositionResult.IsSuccess == false)
        {
            return NotFound(EmployeePositionResult.Error);
        }

        return Ok(EmployeePositionResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeePositionDto>> UpdateEmployeePosition([FromBody] EmployeePositionUpdateDto dto, CancellationToken cancellationToken)
    {
        var EmployeePositionResult = await service.UpdateEmployeePosition(dto, cancellationToken);

        if (EmployeePositionResult.IsSuccess == false)
        {
            return BadRequest(EmployeePositionResult.Error);
        }

        return Ok(EmployeePositionResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<EmployeePositionDto>> CreateEmployeePosition([FromBody] EmployeePositionCreateDto dto, CancellationToken cancellationToken)
    {
        var EmployeePositionResult = await service.CreateEmployeePosition(dto, cancellationToken);

        if (EmployeePositionResult.IsSuccess == false)
        {
            return BadRequest(EmployeePositionResult.Error);
        }

        return Ok(EmployeePositionResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteEmployeePosition(int id, CancellationToken cancellationToken)
    {
        var EmployeePositionResult = await service.DeleteEmployeePosition(id, cancellationToken);

        if (EmployeePositionResult.IsSuccess == false)
        {
            return NotFound(EmployeePositionResult.Error);
        }

        return NoContent();
    }

}