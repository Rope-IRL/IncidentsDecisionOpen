using IncidentsDecision.Core.Models.NotResolvedIncident;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncidentsDecision.Persistence.Configurations
{
    public class NotResolvedIncidentConfiguration: IEntityTypeConfiguration<NotResolvedIncident>
    {
        public void Configure(EntityTypeBuilder<NotResolvedIncident> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasMaxLength(1000);

            builder.Property(e => e.CreatedAt);
        }
    }
}
