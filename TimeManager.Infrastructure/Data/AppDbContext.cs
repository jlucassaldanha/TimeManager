using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Infrastructure.Data.Converters;

namespace TimeManager.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TimeRecord> TimeRecords { get; set; }
    public DbSet<TimeAllowance> TimeAllowances { get; set; }
    public DbSet<WorkJourneyRule> WorkJourneyRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
		
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<TimeAllowance>()
            .Property(a => a.Date)
            .HasColumnType("date"); 
    }

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion<UtcDateTimeConverter>();
	}
}