using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.EmployeePosition;
using IncidentsDecision.Core.Models.Position;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncidentsDecision.Persistence.Configurations;

public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne<Employee>()
            .WithOne()
            .HasForeignKey<EmployeePosition>(e => e.EmployeeId);

        builder.HasOne<Position>()
            .WithOne()
            .HasForeignKey<EmployeePosition>(e => e.PositionId);
    }
}