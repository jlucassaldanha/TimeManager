using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeAllowanceRepository
{
	Task<IEnumerable<TimeAllowance>> GetByUserIdAndDateAllowanceAsync(Guid userId, DateOnly date);
	Task AddAsync(TimeAllowance allowance);
    Task UpdateAsync(TimeAllowance allowance);
}