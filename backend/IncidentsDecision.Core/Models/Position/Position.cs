using IncidentsDecision.Core;
using IncidentsDecision.Core.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace IncidentsDecision.Core.Models.Position;

public class Position
{
    public int? Id { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    private Position(int? id, string name, string description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }

    public static Result<Position> Create(int? id, string name, string description)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
        {
            return Result<Position>.Failure("Name and Description must be not empty");
        }

        var position = new Position(id, name, description);

        return Result<Position>.Success(position);
    }

    public void UpdateName(string name)
    {
        this.Name = name;
    }

    public void UpdateDescription(string description)
    {
        this.Description = description;
    }
}