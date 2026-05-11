using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeRecordRepository
{
	Task AddAsync(TimeRecord record);
	Task UpdateAsync(TimeRecord record);
	Task<TimeRecord?> GetByIdAsync(Guid id);
	Task<IEnumerable<TimeRecord>> GetRecordsByUserIdAndDateAsync(Guid userId, DateTime date);
	Task<IEnumerable<TimeRecord>> GetByUserIdAndPeriodAsync(Guid userId, DateTime startDate, DateTime endDate);
	Task<bool> ExistsPunchAtAsync(Guid userId, DateTime timestamp);
}