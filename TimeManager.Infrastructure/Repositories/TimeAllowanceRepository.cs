using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class TimeAllowanceRepository(AppDbContext context) : ITimeAllowanceRepository
{
    public async Task<IEnumerable<TimeAllowance>> GetByUserIdAndDateAllowanceAsync(Guid userId, DateOnly date)
    {
        return await context.TimeAllowances
            .Where(a => a.UserId == userId 
                    && !a.IsDeleted 
                    && a.Date == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<TimeAllowance>> GetByUserIdAndPeriodAsync(Guid userId, DateOnly startDate, DateOnly endDate)
    {
        return await context.TimeAllowances
            .Where(a => a.UserId == userId 
                    && a.Date >= startDate 
                    && a.Date <= endDate)
            .OrderBy(a => a.Date)
            .ToListAsync();
    }
    
    public async Task AddAsync(TimeAllowance allowance)
    {
        await context.TimeAllowances.AddAsync(allowance);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TimeAllowance allowance)
    {
        context.TimeAllowances.Update(allowance);
        await context.SaveChangesAsync();
    }
}