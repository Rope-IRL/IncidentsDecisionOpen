using IncidentsDecision.Core;
using IncidentsDecision.Core.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace IncidentsDecision.Core.Models.EmployeeLogin;

public class EmployeeLogin
{
    public int? Id { get; }

    public string Login { get; private set; }

    public string HashedPassword { get; private set; }

    public int? EmployeeId { get; }

    private EmployeeLogin(int? id, string login, string hashedPassword, int? employeeId)
    {
        this.Id = id;
        this.Login = login;
        this.HashedPassword = hashedPassword;
        this.EmployeeId = employeeId;
    }

    public static Result<EmployeeLogin> Create(int? id, string login, string hashedPassword, int? employeeId)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(hashedPassword))
        {
            return Result<EmployeeLogin>.Failure("Login and Password must be not empty");
        }

        var employeeLogin = new EmployeeLogin(id, login, hashedPassword, employeeId);

        return Result<EmployeeLogin>.Success(employeeLogin);
    }

    public void UpdateLogin(string login)
    {
        this.Login = login;
    }

    public void UpdateHashedPassword(string hashedPassword)
    {
        this.HashedPassword = hashedPassword;
    }
}