using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface IWorkJourneyRuleRepository
{
	Task<WorkJourneyRule?> GetAsync();
	Task AddAsync(WorkJourneyRule rule);
    Task UpdateAsync(WorkJourneyRule rule);
}