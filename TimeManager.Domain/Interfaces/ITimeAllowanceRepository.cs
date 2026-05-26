using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeAllowanceRepository
{
	Task<TimeAllowance?> GetByDateAllowanceAsync(DateOnly date);
	Task<IEnumerable<TimeAllowance>> GetByDateAllowancesAsync(DateOnly date);
	Task<IEnumerable<TimeAllowance>> GetByPeriodAsync(DateOnly startDate, DateOnly endDate);
	Task AddAsync(TimeAllowance allowance);
    Task UpdateAsync(TimeAllowance allowance);
	Task<TimeAllowance?> GetByIdAsync(Guid id);
}