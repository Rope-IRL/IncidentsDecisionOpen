using IncidentsDecision.Application.DTO.NotResolvedIncidentsDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]/")]
public class NotResolvedIncidentController(INotResolvedIncidentService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles ="Employee,TechSupport")]
    public async Task<ActionResult<IEnumerable<NotResolvedIncidentDto>>> GetNotResolvedIncidents(CancellationToken cancellationToken)
    {
        var NotResolvedIncidents = await service.GetNotResolvedIncidents(cancellationToken);

        return Ok(NotResolvedIncidents);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles ="Employee,TechSupport")]
    public async Task<ActionResult<NotResolvedIncidentDto>> GetNotResolvedIncidentById(int id, CancellationToken cancellationToken)
    {
        var NotResolvedIncidentResult = await service.GetNotResolvedIncidentById(id, cancellationToken);
        if (NotResolvedIncidentResult.IsSuccess == false)
        {
            return NotFound(NotResolvedIncidentResult.Error);
        }

        return Ok(NotResolvedIncidentResult.Value);
    }

    [HttpPost]
    [Authorize(Roles ="TechSupport")]
    public async Task<ActionResult<NotResolvedIncidentDto>> UpdateNotResolvedIncident([FromBody] NotResolvedIncidentUpdateDto dto, CancellationToken cancellationToken)
    {
        var NotResolvedIncidentResult = await service.UpdateNotResolvedIncident(dto, cancellationToken);

        if (NotResolvedIncidentResult.IsSuccess == false)
        {
            return BadRequest(NotResolvedIncidentResult.Error);
        }

        return Ok(NotResolvedIncidentResult.Value);
    }

    [HttpPut]
    [Authorize(Roles ="Employee,TechSupport")]
    public async Task<ActionResult<NotResolvedIncidentDto>> CreateNotResolvedIncident([FromBody] NotResolvedIncidentCreateDto dto, CancellationToken cancellationToken)
    {
        var NotResolvedIncidentResult = await service.CreateNotResolvedIncident(dto, cancellationToken);

        if (NotResolvedIncidentResult.IsSuccess == false)
        {
            return BadRequest(NotResolvedIncidentResult.Error);
        }

        return Ok(NotResolvedIncidentResult.Value);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles ="TechSupport")]
    public async Task<ActionResult> DeleteNotResolvedIncident(int id, CancellationToken cancellationToken)
    {
        var NotResolvedIncidentResult = await service.DeleteNotResolvedIncident(id, cancellationToken);

        if (NotResolvedIncidentResult.IsSuccess == false)
        {
            return NotFound(NotResolvedIncidentResult.Error);
        }

        return NoContent();
    }

}