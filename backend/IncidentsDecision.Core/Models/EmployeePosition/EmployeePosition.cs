using System.Runtime.InteropServices;
using IncidentsDecision.Core;
using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Core.Models.EmployeePosition;

public class EmployeePosition
{
    public int? Id { get; }

    public int EmployeeId { get; private set; }

    public int PositionId { get; private set; }

    private EmployeePosition(int? id, int employeeId, int positionId)
    {
        this.Id = id;
        this.EmployeeId = employeeId;
        this.PositionId = positionId;
    }

    public static Result<EmployeePosition> Create(int? id, int employeeId, int positionId)
    {
        if (employeeId == 0 || positionId == 0)
        {
            return Result<EmployeePosition>.Failure("Employee Id and Position Id must be not empty");
        }

        var employeePosition = new EmployeePosition(id, employeeId, positionId);

        return Result<EmployeePosition>.Success(employeePosition);
    }

    public void UpdateEmployeeId(int employeeId)
    {
        this.EmployeeId = employeeId;
    }

    public void UpdatePositionId(int positionId)
    {
        this.PositionId = positionId;
    }
}