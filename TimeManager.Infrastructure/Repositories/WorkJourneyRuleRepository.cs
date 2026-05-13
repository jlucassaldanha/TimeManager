using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class WorkJourneyRuleRepository(AppDbContext context) : IWorkJourneyRuleRepository
{
	public async Task<WorkJourneyRule?> GetAsync()
	{
		return await context.WorkJourneyRules.FirstOrDefaultAsync();
	}

	public async Task AddAsync(WorkJourneyRule rule)
	{
		await context.WorkJourneyRules.AddAsync(rule);
		await context.SaveChangesAsync();
	}

	public async Task UpdateAsync(WorkJourneyRule rule)
	{
		context.WorkJourneyRules.Update(rule);
		await context.SaveChangesAsync();
	}
}