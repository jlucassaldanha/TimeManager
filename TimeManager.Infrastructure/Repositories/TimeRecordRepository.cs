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

	public async Task<IEnumerable<TimeRecord>> GetRecordsByUserIdAndDateAsync(Guid userId, DateTime date)
	{
		return await context.TimeRecords
			.Where(r => r.UserId == userId
					&& r.Timestamp.Date == date.Date
					&& !r.IsDeleted)
			.OrderBy(t => t.Timestamp)
			.ToListAsync();
	}

	public async Task<TimeRecord?> GetByIdAsync(Guid id)
	{
		return await context.TimeRecords
			.Where(r => r.Id == id)
			.FirstOrDefaultAsync();
	}

	public async Task UpdateAsync(TimeRecord record)
    {
        context.TimeRecords.Update(record);
        await context.SaveChangesAsync();
    }
}