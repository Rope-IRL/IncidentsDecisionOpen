using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncidentsDecision.Persistence.Configurations;

public class HealthGroupConfiguration : IEntityTypeConfiguration<HealthGroup>
{
    public void Configure(EntityTypeBuilder<HealthGroup> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasMaxLength(80);

    }
}