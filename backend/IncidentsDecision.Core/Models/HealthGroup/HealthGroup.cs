using IncidentsDecision.Core;
using IncidentsDecision.Core.Helpers;
using Microsoft.IdentityModel.Tokens;

public class HealthGroup
{
    public int? Id { get; }

    public string Name { get; }

    public string Description { get; }

    private HealthGroup(int? id, string name, string description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }


    public static Result<HealthGroup> Create(int? id, string name, string description)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
        {
            return Result<HealthGroup>.Failure("Name and Description must be not empty");
        }

        var healthGroup = new HealthGroup(id, name, description);

        return Result<HealthGroup>.Success(healthGroup);
    }
}