using IncidentsDecision.Application.DTO.EmployeeLoginDtos;
using IncidentsDecision.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncidentsDecision.Api.Controllers;

[ApiController]
[Authorize(Roles ="Employee")]
[Route("/api/[controller]/")]
public class EmployeeLoginController(IEmployeeLoginService service, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeLoginDto>>> GetEmployeeLogins(CancellationToken cancellationToken)
    {
        var EmployeeLogins = await service.GetEmployeeLogins(cancellationToken);

        return Ok(EmployeeLogins);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeLoginDto>> GetEmployeeLoginById(int id, CancellationToken cancellationToken)
    {
        var EmployeeLoginResult = await service.GetEmployeeLoginById(id, cancellationToken);
        if (EmployeeLoginResult.IsSuccess == false)
        {
            return NotFound(EmployeeLoginResult.Error);
        }

        return Ok(EmployeeLoginResult.Value);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeLoginDto>> UpdateEmployeeLogin([FromBody] EmployeeLoginUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var EmployeeLoginResult = await service.UpdateEmployeeLogin(dto, cancellationToken);

        if (EmployeeLoginResult.IsSuccess == false)
        {
            return BadRequest(EmployeeLoginResult.Error);
        }

        return Ok(EmployeeLoginResult.Value);
    }

    [HttpPut]
    public async Task<ActionResult<EmployeeLoginDto>> CreateEmployeeLogin([FromBody] EmployeeLoginCreateDto dto,
        CancellationToken cancellationToken)
    {
        var EmployeeLoginResult = await service.CreateEmployeeLogin(dto, cancellationToken);

        if (EmployeeLoginResult.IsSuccess == false)
        {
            return BadRequest(EmployeeLoginResult.Error);
        }

        return Ok(EmployeeLoginResult.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteEmployeeLogin(int id, CancellationToken cancellationToken)
    {
        var EmployeeLoginResult = await service.DeleteEmployeeLogin(id, cancellationToken);

        if (EmployeeLoginResult.IsSuccess == false)
        {
            return NotFound(EmployeeLoginResult.Error);
        }

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginEmployee([FromBody] EmployeeLoginCreateDto dto, CancellationToken cancellationToken)
    {
        var tokenResult = await service.LoginEmployee(dto.Login, dto.HashedPassword, cancellationToken);

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