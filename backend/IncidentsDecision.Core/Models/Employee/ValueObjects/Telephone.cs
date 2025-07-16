using IncidentsDecision.Core.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace IncidentsDecision.Core.Models.Employee.ValueObjects;

public class Telephone : ValueObject
{
    private const string phoneRegex = @"";

    public string Number { get; }

    private Telephone(string number)
    {
        this.Number = number;
    }

    public static Result<Telephone> Create(string number)
    {
        if (string.IsNullOrEmpty(number))
        {
            return Result<Telephone>.Failure("Phone number can't be empty");
        }

        var phoneNumber = new Telephone(number);

        return Result<Telephone>.Success(phoneNumber);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}