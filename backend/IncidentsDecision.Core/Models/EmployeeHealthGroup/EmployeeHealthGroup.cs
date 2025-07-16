using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Core.Models.EmployeeHealthGroup;

public class EmployeeHealthGroup
{
    public int? Id { get; }

    public int EmployeeId { get; private set; }

    public int HealthGroupId { get; private set; }

    private EmployeeHealthGroup(int? id, int employeeId, int healthGroupId)
    {
        this.Id = id;
        this.EmployeeId = employeeId;
        this.HealthGroupId = healthGroupId;
    }

    public static Result<EmployeeHealthGroup> Create(int? id, int employeeId, int healthGroupId)
    {
        if (employeeId == 0 || healthGroupId == 0)
        {
            return Result<EmployeeHealthGroup>.Failure("Employee Id and Health Group Id must be not empty");
        }

        var employeeHealthGroup = new EmployeeHealthGroup(id, employeeId, healthGroupId);

        return Result<EmployeeHealthGroup>.Success(employeeHealthGroup);
    }

    public void UpdateEmployeeId(int employeeId)
    {
        this.EmployeeId = employeeId;
    }

    public void UpdateHealthGroupId(int healthGroupId)
    {
        this.HealthGroupId = healthGroupId;
    }
}