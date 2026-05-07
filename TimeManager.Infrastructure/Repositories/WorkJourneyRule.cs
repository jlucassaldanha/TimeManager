using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class WorkJourneyRuleRepository(AppDbContext context) : IWorkJourneyRuleRepository
{
	private readonly AppDbContext _context = context;

	public async Task<WorkJourneyRule?> GetByUserIdAsync(Guid userId)
	{
		return await _context.WorkJourneyRules.Where(w => w.UserId == userId).FirstOrDefaultAsync();
	}

	public async Task AddAsync(WorkJourneyRule rule)
	{
		await _context.WorkJourneyRules.AddAsync(rule);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(WorkJourneyRule rule)
	{
		_context.WorkJourneyRules.Update(rule);
		await _context.SaveChangesAsync();
	}
}