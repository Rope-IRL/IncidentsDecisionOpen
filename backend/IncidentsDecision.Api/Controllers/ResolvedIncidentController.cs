using IncidentsDecision.Application.DTO.ResolvedIncidentDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/")]
public class ResolvedIncidentController(IResolvedIncidentService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles ="Employee,TechSupport")]
    public async Task<ActionResult<IEnumerable<ResolvedIncidentDto>>> GetResolvedIncidents(CancellationToken cancellationToken)
    {
        var ResolvedIncidents = await service.GetResolvedIncidents(cancellationToken);

        return Ok(ResolvedIncidents);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles ="Employee,TechSupport")]
    public async Task<ActionResult<ResolvedIncidentDto>> GetResolvedIncidentById(int id, CancellationToken cancellationToken)
    {
        var ResolvedIncidentResult = await service.GetResolvedIncidentById(id, cancellationToken);
        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return NotFound(ResolvedIncidentResult.Error);
        }

        return Ok(ResolvedIncidentResult.Value);
    }

    [HttpPost]
    [Authorize(Roles ="TechSupport")]
    public async Task<ActionResult<ResolvedIncidentDto>> UpdateResolvedIncident([FromBody] ResolvedIncidentUpdateDto dto, CancellationToken cancellationToken)
    {
        var ResolvedIncidentResult = await service.UpdateResolvedIncident(dto, cancellationToken);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return BadRequest(ResolvedIncidentResult.Error);
        }

        return Ok(ResolvedIncidentResult.Value);
    }

    [HttpPut]
    [Authorize(Roles ="TechSupport")]
    public async Task<ActionResult<ResolvedIncidentDto>> CreateResolvedIncident([FromBody] ResolvedIncidentCreateDto dto, CancellationToken cancellationToken)
    {
        var ResolvedIncidentResult = await service.CreateResolvedIncident(dto, cancellationToken);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return BadRequest(ResolvedIncidentResult.Error);
        }

        return Ok(ResolvedIncidentResult.Value);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles ="TechSupport")]
    public async Task<ActionResult> DeleteResolvedIncident(int id, CancellationToken cancellationToken)
    {
        var ResolvedIncidentResult = await service.DeleteResolvedIncident(id, cancellationToken);

        if (ResolvedIncidentResult.IsSuccess == false)
        {
            return NotFound(ResolvedIncidentResult.Error);
        }

        return NoContent();
    }

}