using IncidentsDecision.Application.DTO.HealthGroupDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class HealthGroupController(IHealthGroupService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthGroupDto>>> GetHealthGroups(CancellationToken cancellationToken)
    {
        var HealthGroups = await service.GetHealthGroups(cancellationToken);

        return Ok(HealthGroups);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HealthGroupDto>> GetHealthGroupById(int id, CancellationToken cancellationToken)
    {
        var HealthGroupResult = await service.GetHealthGroupById(id, cancellationToken);
        if (HealthGroupResult.IsSuccess == false)
        {
            return NotFound(HealthGroupResult.Error);
        }

        return Ok(HealthGroupResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<HealthGroupDto>> UpdateHealthGroup([FromBody] HealthGroupUpdateDto dto, CancellationToken cancellationToken)
    {
        var HealthGroupResult = await service.UpdateHealthGroup(dto, cancellationToken);

        if (HealthGroupResult.IsSuccess == false)
        {
            return BadRequest(HealthGroupResult.Error);
        }

        return Ok(HealthGroupResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<HealthGroupDto>> CreateHealthGroup([FromBody] HealthGroupCreateDto dto, CancellationToken cancellationToken)
    {
        var HealthGroupResult = await service.CreateHealthGroup(dto, cancellationToken);

        if (HealthGroupResult.IsSuccess == false)
        {
            return BadRequest(HealthGroupResult.Error);
        }

        return Ok(HealthGroupResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteHealthGroup(int id, CancellationToken cancellationToken)
    {
        var HealthGroupResult = await service.DeleteHealthGroup(id, cancellationToken);

        if (HealthGroupResult.IsSuccess == false)
        {
            return NotFound(HealthGroupResult.Error);
        }

        return NoContent();
    }

}