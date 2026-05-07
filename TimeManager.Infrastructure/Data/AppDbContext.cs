using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;

namespace TimeManager.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<User> Users { get; set; }
    public DbSet<TimeRecord> TimeRecords { get; set; }
    public DbSet<TimeAllowance> TimeAllowances { get; set; }
    public DbSet<WorkJourneyRule> WorkJourneyRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
		
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}