using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface ITimeAllowanceRepository
{
	Task<TimeAllowance?> GetValidAllowanceAsync(Guid userId, DateTime date);
	Task AddAsync(TimeAllowance allowance);
    Task UpdateAsync(TimeAllowance allowance);
}