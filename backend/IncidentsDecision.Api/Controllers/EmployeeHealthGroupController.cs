using IncidentsDecision.Application.DTO.EmployeeHealthGroupDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class EmployeeHealthGroupController(IEmployeeHealthGroupService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeHealthGroupDto>>> GetEmployeeHealthGroups(CancellationToken cancellationToken)
    {
        var EmployeeHealthGroups = await service.GetEmployeeHealthGroups(cancellationToken);

        return Ok(EmployeeHealthGroups);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeHealthGroupDto>> GetEmployeeHealthGroupById(int id, CancellationToken cancellationToken)
    {
        var EmployeeHealthGroupResult = await service.GetEmployeeHealthGroupById(id, cancellationToken);
        if (EmployeeHealthGroupResult.IsSuccess == false)
        {
            return NotFound(EmployeeHealthGroupResult.Error);
        }

        return Ok(EmployeeHealthGroupResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeHealthGroupDto>> UpdateEmployeeHealthGroup([FromBody] EmployeeHealthGroupUpdateDto dto, CancellationToken cancellationToken)
    {
        var EmployeeHealthGroupResult = await service.UpdateEmployeeHealthGroup(dto, cancellationToken);

        if (EmployeeHealthGroupResult.IsSuccess == false)
        {
            return BadRequest(EmployeeHealthGroupResult.Error);
        }

        return Ok(EmployeeHealthGroupResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<EmployeeHealthGroupDto>> CreateEmployeeHealthGroup([FromBody] EmployeeHealthGroupCreateDto dto, CancellationToken cancellationToken)
    {
        var EmployeeHealthGroupResult = await service.CreateEmployeeHealthGroup(dto, cancellationToken);

        if (EmployeeHealthGroupResult.IsSuccess == false)
        {
            return BadRequest(EmployeeHealthGroupResult.Error);
        }

        return Ok(EmployeeHealthGroupResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteEmployeeHealthGroup(int id, CancellationToken cancellationToken)
    {
        var EmployeeHealthGroupResult = await service.DeleteEmployeeHealthGroup(id, cancellationToken);

        if (EmployeeHealthGroupResult.IsSuccess == false)
        {
            return NotFound(EmployeeHealthGroupResult.Error);
        }

        return NoContent();
    }

}