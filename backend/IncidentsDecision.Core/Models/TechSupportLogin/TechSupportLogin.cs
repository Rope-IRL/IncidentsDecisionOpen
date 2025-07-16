using IncidentsDecision.Core.Helpers;
using Microsoft.IdentityModel.Tokens;

public class TechSupportLogin
{
    public int? Id { get; }

    public string Login { get; private set; }

    public string HashedPassword { get; private set; }

    public int? SupportId { get; }


    private TechSupportLogin(int? id, string login, string hashedPassword, int? supportId)
    {
        this.Id = id;
        this.Login = login;
        this.HashedPassword = hashedPassword;
        this.SupportId = supportId;
    }

    public static Result<TechSupportLogin> Create(int? id, string login, string hashedPassword, int? supportId)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(hashedPassword))
        {
            return Result<TechSupportLogin>.Failure("Login and Password must be not empty");
        }

        var techSupportLogin = new TechSupportLogin(id, login, hashedPassword, supportId);

        return Result<TechSupportLogin>.Success(techSupportLogin);
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