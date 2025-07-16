using IncidentsDecision.Application.DTO.TechSupportDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Authorize(Roles ="TechSupport")]
[Route("/api/[controller]/")]
public class TechSupportController(ITechSupportService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TechSupportDto>>> GetTechSupports(CancellationToken cancellationToken)
    {
        var TechSupports = await service.GetTechSupports(cancellationToken);

        return Ok(TechSupports);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TechSupportDto>> GetTechSupportById(int id, CancellationToken cancellationToken)
    {
        var TechSupportResult = await service.GetTechSupportById(id, cancellationToken);
        if (TechSupportResult.IsSuccess == false)
        {
            return NotFound(TechSupportResult.Error);
        }

        return Ok(TechSupportResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<TechSupportDto>> UpdateTechSupport([FromBody] TechSupportUpdateDto dto, CancellationToken cancellationToken)
    {
        var TechSupportResult = await service.UpdateTechSupport(dto, cancellationToken);

        if (TechSupportResult.IsSuccess == false)
        {
            return BadRequest(TechSupportResult.Error);
        }

        return Ok(TechSupportResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<TechSupportDto>> CreateTechSupport([FromBody] TechSupportCreateDto dto, CancellationToken cancellationToken)
    {
        var TechSupportResult = await service.CreateTechSupport(dto, cancellationToken);

        if (TechSupportResult.IsSuccess == false)
        {
            return BadRequest(TechSupportResult.Error);
        }

        return Ok(TechSupportResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTechSupport(int id, CancellationToken cancellationToken)
    {
        var TechSupportResult = await service.DeleteTechSupport(id, cancellationToken);

        if (TechSupportResult.IsSuccess == false)
        {
            return NotFound(TechSupportResult.Error);
        }

        return NoContent();
    }

    [HttpGet("info")]
    public async Task<ActionResult> GetTechSupportByIdFromToken(CancellationToken cancellationToken)
    {
        Console.WriteLine("I executed");

        var rawEmployeeId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

        if (rawEmployeeId == null)
        {
            return NotFound("Can't find such employee");
        }

        int employeeId = int.Parse(rawEmployeeId.Value);

        var employeeResult = await service.GetTechSupportById(employeeId, cancellationToken);

        if (employeeResult.IsSuccess == false)
        {
            return NotFound(employeeResult.Error);
        }

        return Ok(employeeResult.Value);
    }
}