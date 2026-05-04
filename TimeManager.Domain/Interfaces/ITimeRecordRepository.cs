using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeRecordRepository
{
	Task AddAsync(TimeRecord record);
	Task<IEnumerable<TimeRecord>> GetRecordsByUserIdAndDateAsync(Guid userId, DateTime date);
}