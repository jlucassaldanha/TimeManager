using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class TimeRecordRepository(AppDbContext context) : ITimeRecordRepository
{
	public async Task AddAsync(TimeRecord record)
	{
		await context.TimeRecords.AddAsync(record);
		await context.SaveChangesAsync();
	}

	public async Task<IEnumerable<TimeRecord>> GetRecordsByDateAsync(DateTime date)
	{
		return await context.TimeRecords
			.Where(r => r.Timestamp.Date == date.Date
					&& !r.IsDeleted)
			.OrderBy(t => t.Timestamp)
			.ToListAsync();
	}

	public async Task<IEnumerable<TimeRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await context.TimeRecords
            .Where(a => a.Timestamp >= startDate 
                    && a.Timestamp <= endDate)
            .OrderBy(a => a.Timestamp)
            .ToListAsync();
    }

	public async Task<TimeRecord?> GetByIdAsync(Guid id)
	{
		return await context.TimeRecords
			.Where(r => r.Id == id && !r.IsDeleted)
			.FirstOrDefaultAsync();
	}

	public async Task UpdateAsync(TimeRecord record)
    {
        context.TimeRecords.Update(record);
        await context.SaveChangesAsync();
    }

	public async Task<bool> ExistsPunchAtAsync(DateTime timestamp)
	{
		return await context.TimeRecords
			.AnyAsync(t => !t.IsDeleted && t.Timestamp == timestamp);
	}
}