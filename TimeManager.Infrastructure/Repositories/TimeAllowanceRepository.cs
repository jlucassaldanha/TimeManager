using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class TimeAllowanceRepository(AppDbContext context) : ITimeAllowanceRepository
{
    public async Task<TimeAllowance?> GetValidAllowanceAsync(Guid userId, DateTime date)
    {
        return await context.TimeAllowances
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Date.Date == date.Date);
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