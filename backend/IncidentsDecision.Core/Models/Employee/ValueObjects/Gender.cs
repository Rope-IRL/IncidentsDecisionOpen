using System.Runtime.InteropServices;
using IncidentsDecision.Core.Helpers;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace IncidentsDecision.Core.Models.Employee.ValueObjects;

public class Gender : ValueObject
{
    public static readonly Gender Male = new(nameof(Male));
    public static readonly Gender Female = new(nameof(Female));

    private static readonly Gender[] _all = [Male, Female];
    public string Value { get; }

    private Gender(string value)
    {
        this.Value = value;
    }

    public static Result<Gender> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<Gender>.Failure("Gender can't be empty");
        }

        var gender = value.Trim().ToLower();

        if (_all.Any(val => val.Value.ToLower() == gender) == false)
        {
            return Result<Gender>.Failure("Gender has to be male or female");
        }

        return Result<Gender>.Success(new Gender(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}