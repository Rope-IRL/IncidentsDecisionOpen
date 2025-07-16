using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.EmployeeLogin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace IncidentsDecision.Persistence.Configurations;

public class EmployeeLoginConfiguration : IEntityTypeConfiguration<EmployeeLogin>
{
    public void Configure(EntityTypeBuilder<EmployeeLogin> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Login)
            .HasMaxLength(100);

        builder.Property(e => e.HashedPassword)
            .HasMaxLength(200);

        builder.HasOne<Employee>()
            .WithOne()
            .HasForeignKey<EmployeeLogin>(e => e.EmployeeId);
    }
}