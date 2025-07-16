using IncidentsDecision.Application.DTO.PositionDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class PositionController(IPositionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions(CancellationToken cancellationToken)
    {
        var Positions = await service.GetPositions(cancellationToken);

        return Ok(Positions);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PositionDto>> GetPositionById(int id, CancellationToken cancellationToken)
    {
        var PositionResult = await service.GetPositionById(id, cancellationToken);
        if (PositionResult.IsSuccess == false)
        {
            return NotFound(PositionResult.Error);
        }

        return Ok(PositionResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<PositionDto>> UpdatePosition([FromBody] PositionUpdateDto dto, CancellationToken cancellationToken)
    {
        var PositionResult = await service.UpdatePosition(dto, cancellationToken);

        if (PositionResult.IsSuccess == false)
        {
            return BadRequest(PositionResult.Error);
        }

        return Ok(PositionResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<PositionDto>> CreatePosition([FromBody] PositionCreateDto dto, CancellationToken cancellationToken)
    {
        var PositionResult = await service.CreatePosition(dto, cancellationToken);

        if (PositionResult.IsSuccess == false)
        {
            return BadRequest(PositionResult.Error);
        }

        return Ok(PositionResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePosition(int id, CancellationToken cancellationToken)
    {
        var PositionResult = await service.DeletePosition(id, cancellationToken);

        if (PositionResult.IsSuccess == false)
        {
            return NotFound(PositionResult.Error);
        }

        return NoContent();
    }

}