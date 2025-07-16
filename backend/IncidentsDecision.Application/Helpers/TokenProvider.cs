using System.Security.Claims;
using System.Text;
using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.TechSupport;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(Employee employee)
    {
        string secretKey = configuration["JWT:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("id", employee.Id.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Name, employee.Name),
                new Claim(ClaimTypes.Role, "Employee")
            ]),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["JWT:ExpirationInMinutes"])),
            SigningCredentials = credentials,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string CreateForTechSup(TechSupport support)
    {
        string secretKey = configuration["JWT:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("id", support.Id.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Name, support.Name),
                new Claim(ClaimTypes.Role, "TechSupport")
            ]),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["JWT:ExpirationInMinutes"])),
            SigningCredentials = credentials,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}