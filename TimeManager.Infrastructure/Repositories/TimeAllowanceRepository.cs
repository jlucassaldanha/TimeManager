using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class TimeAllowanceRepository(AppDbContext context) : ITimeAllowanceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<TimeAllowance?> GetValidAllowanceAsync(Guid userId, DateTime date)
    {
        return await _context.TimeAllowances
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Date.Date == date.Date);
    }
    
    public async Task AddAsync(TimeAllowance allowance)
    {
        await _context.TimeAllowances.AddAsync(allowance);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TimeAllowance allowance)
    {
        _context.TimeAllowances.Update(allowance);
        await _context.SaveChangesAsync();
    }
}