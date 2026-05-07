using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class WorkJourneyRuleRepository(AppDbContext context) : IWorkJourneyRuleRepository
{
	public async Task<WorkJourneyRule?> GetByUserIdAsync(Guid userId)
	{
		return await context.WorkJourneyRules.Where(w => w.UserId == userId).FirstOrDefaultAsync();
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