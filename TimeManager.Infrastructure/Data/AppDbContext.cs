using Microsoft.EntityFrameworkCore;
using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Infrastructure.Data.Converters;

namespace TimeManager.Infrastructure.Data;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    ICurrentUserService currentUserService) : DbContext(options)
{
    public DbSet<TimeRecord> TimeRecords { get; set; }
    public DbSet<TimeAllowance> TimeAllowances { get; set; }
    public DbSet<WorkJourneyRule> WorkJourneyRules { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
		
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<TimeAllowance>()
            .Property(a => a.Date)
            .HasColumnType("date"); 

        modelBuilder.Entity<TimeAllowance>()
            .HasQueryFilter(t => t.UserId == currentUserService.UserId && !t.IsDeleted);

        modelBuilder.Entity<TimeRecord>()
            .HasQueryFilter(t => t.UserId == currentUserService.UserId && !t.IsDeleted);

        modelBuilder.Entity<WorkJourneyRule>()
            .HasQueryFilter(w => w.UserId == currentUserService.UserId);
    }

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion<UtcDateTimeConverter>();
	}
}