using IncidentsDecision.Application.DTO.TechSupportLoginDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Authorize(Roles ="TechSupport")]
[Route("/api/[controller]/")]
public class TechSupportLoginController(ITechSupportLoginService service, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TechSupportLoginDto>>> GetTechSupportLogins(CancellationToken cancellationToken)
    {
        var TechSupportLogins = await service.GetTechSupportLogins(cancellationToken);

        return Ok(TechSupportLogins);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TechSupportLoginDto>> GetTechSupportLoginById(int id, CancellationToken cancellationToken)
    {
        var TechSupportLoginResult = await service.GetTechSupportLoginById(id, cancellationToken);
        if (TechSupportLoginResult.IsSuccess == false)
        {
            return NotFound(TechSupportLoginResult.Error);
        }

        return Ok(TechSupportLoginResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<TechSupportLoginDto>> UpdateTechSupportLogin([FromBody] TechSupportLoginUpdateDto dto, CancellationToken cancellationToken)
    {
        var TechSupportLoginResult = await service.UpdateTechSupportLogin(dto, cancellationToken);

        if (TechSupportLoginResult.IsSuccess == false)
        {
            return BadRequest(TechSupportLoginResult.Error);
        }

        return Ok(TechSupportLoginResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<TechSupportLoginDto>> CreateTechSupportLogin([FromBody] TechSupportLoginCreateDto dto, CancellationToken cancellationToken)
    {
        var TechSupportLoginResult = await service.CreateTechSupportLogin(dto, cancellationToken);

        if (TechSupportLoginResult.IsSuccess == false)
        {
            return BadRequest(TechSupportLoginResult.Error);
        }

        return Ok(TechSupportLoginResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTechSupportLogin(int id, CancellationToken cancellationToken)
    {
        var TechSupportLoginResult = await service.DeleteTechSupportLogin(id, cancellationToken);

        if (TechSupportLoginResult.IsSuccess == false)
        {
            return NotFound(TechSupportLoginResult.Error);
        }

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginTechSupport([FromBody] TechSupportLoginCreateDto dto,
        CancellationToken cancellationToken)
    {
        var tokenResult = await service.LoginTechSupport(dto.Login, dto.HashedPassword, cancellationToken);

        if (tokenResult.IsSuccess == false)
        {
            return BadRequest(tokenResult.Error);
        }

        HttpContext.Response.Cookies.Append(configuration["JWT:Name"], tokenResult.Value, new CookieOptions
        {
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Cookie:ExpirationInMinutes"])),
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax
        });

        return Ok();
    }
}