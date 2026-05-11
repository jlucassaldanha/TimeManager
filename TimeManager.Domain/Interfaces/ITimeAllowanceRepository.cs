using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeAllowanceRepository
{
	Task<IEnumerable<TimeAllowance>> GetByUserIdAndDateAllowanceAsync(Guid userId, DateOnly date);
	Task<IEnumerable<TimeAllowance>> GetByUserIdAndPeriodAsync(Guid userId, DateOnly startDate, DateOnly endDate);
	Task AddAsync(TimeAllowance allowance);
    Task UpdateAsync(TimeAllowance allowance);
}