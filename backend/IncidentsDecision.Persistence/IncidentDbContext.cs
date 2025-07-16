using IncidentsDecision.Core.Models.EmployeePosition;
using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.EmployeeLogin;
using IncidentsDecision.Core.Models.EmployeeHealthGroup;
using IncidentsDecision.Core.Models.Position;
using IncidentsDecision.Core.Models.TechSupport;
using Microsoft.EntityFrameworkCore;
using IncidentsDecision.Persistence.Configurations;
using IncidentsDecision.Core.Models.NotResolvedIncident;
using IncidentsDecision.Core.Models.ResolvedIncident;

public class IncidentDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<HealthGroup> HealthGroups { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<EmployeeLogin> EmployeeLogins { get; set; }
    public DbSet<EmployeeHealthGroup> EmployeeHealthGroups { get; set; }
    public DbSet<EmployeePosition> EmployeePositions { get; set; }
    public DbSet<TechSupport> TechSupports { get; set; }
    public DbSet<TechSupportLogin> TechSupportLogins { get; set; }
    public DbSet<NotResolvedIncident> NotResolvedIncidents { get; set; }
    public DbSet<ResolvedIncident> ResolvedIncidents { get; set; }
    public IncidentDbContext(DbContextOptions<IncidentDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeHealthGroupConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeLoginConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeePositionConfiguration());
        modelBuilder.ApplyConfiguration(new HealthGroupConfiguration());
        modelBuilder.ApplyConfiguration(new PositionConfiguration());
        modelBuilder.ApplyConfiguration(new TechSupportConfiguration());
        modelBuilder.ApplyConfiguration(new TechSupportLoginConfiguration());
        modelBuilder.ApplyConfiguration(new NotResolvedIncidentConfiguration());
        modelBuilder.ApplyConfiguration(new ResolvedIncidentConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}