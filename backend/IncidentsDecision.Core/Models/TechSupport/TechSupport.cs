using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.TechSupport.ValueObjects;
using Microsoft.Identity.Client;

namespace IncidentsDecision.Core.Models.TechSupport;

public class TechSupport
{
    public int? Id { get; }

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public Telephone Telephone { get; private set; }

    public int? LoginId { get; }

    private TechSupport(int? id, string name, string surname, Telephone telephone, int? loginId)
    {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.Telephone = telephone;
        this.LoginId = loginId;
    }

    public static Result<TechSupport> Create(int? id, string name, string surname, string telephone, int? loginId)
    {
        var telephoneResult = Telephone.Create(telephone);
        if (telephoneResult.IsSuccess == false)
        {
            return Result<TechSupport>.Failure(telephoneResult.Error);
        }

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
        {
            return Result<TechSupport>.Failure("Name and surname must be not empty");
        }

        var techSupport = new TechSupport(id, name, surname, telephoneResult.Value, loginId);

        return Result<TechSupport>.Success(techSupport);
    }

    public void UpdateName(string name)
    {
        this.Name = name;
    }

    public void UpdateSurname(string surname)
    {
        this.Surname = surname;
    }

    public void UpdateTelephone(Telephone telephone)
    {
        this.Telephone = telephone;
    }
}