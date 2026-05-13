using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeRecordRepository
{
	Task AddAsync(TimeRecord record);
	Task UpdateAsync(TimeRecord record);
	Task<TimeRecord?> GetByIdAsync(Guid id);
	Task<IEnumerable<TimeRecord>> GetRecordsByDateAsync(DateTime date);
	Task<IEnumerable<TimeRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
	Task<bool> ExistsPunchAtAsync(DateTime timestamp);
}