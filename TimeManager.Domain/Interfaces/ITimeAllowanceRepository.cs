using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeAllowanceRepository
{
	Task<IEnumerable<TimeAllowance>> GetByDateAllowanceAsync(DateOnly date);
	Task<IEnumerable<TimeAllowance>> GetByPeriodAsync(DateOnly startDate, DateOnly endDate);
	Task AddAsync(TimeAllowance allowance);
    Task UpdateAsync(TimeAllowance allowance);
}