using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.Employee.ValueObjects;
using IncidentsDecision.Core.Models.EmployeeLogin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncidentsDecision.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(30);

        builder.Property(e => e.Surname)
            .HasMaxLength(40);
            
        builder.Property(e => e.Telephone)
            .HasConversion(telephone => telephone.Number,
                value => Telephone.Create(value).Value);

        builder.Property(e => e.Gender)
            .HasConversion(gender => gender.Value,
                value => Gender.Create(value).Value);

        builder.HasOne<EmployeeLogin>()
            .WithOne()
            .HasForeignKey<Employee>(e => e.LoginId);

    }
}