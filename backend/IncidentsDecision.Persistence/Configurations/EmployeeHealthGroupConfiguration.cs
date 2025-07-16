using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncidentsDecision.Persistence.Configurations;

public class EmployeeHealthGroupConfiguration : IEntityTypeConfiguration<EmployeeHealthGroup>
{
    public void Configure(EntityTypeBuilder<EmployeeHealthGroup> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne<Employee>()
            .WithOne()
            .HasForeignKey<EmployeeHealthGroup>(e => e.EmployeeId);

        builder.HasOne<HealthGroup>()
            .WithOne()
            .HasForeignKey<EmployeeHealthGroup>(e => e.HealthGroupId); 
    }
}