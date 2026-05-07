using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class TimeRecordRepository(AppDbContext context) : ITimeRecordRepository
{
	private readonly AppDbContext _context = context;

	public async Task AddAsync(TimeRecord record)
	{
		await _context.TimeRecords.AddAsync(record);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<TimeRecord>> GetRecordsByUserIdAndDateAsync(Guid userId, DateTime date)
	{
		return await _context.TimeRecords
			.Where(r => r.UserId == userId
					&& r.Timestamp.Date == date.Date
					&& !r.IsDeleted)
			.OrderBy(t => t.Timestamp)
			.ToListAsync();
	}
}