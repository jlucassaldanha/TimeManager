using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;

namespace TimeManager.Application.UseCases;

public class CreateAllowanceUseCase(
	ITimeAllowanceRepository allowanceRepository,
	IWorkJourneyRuleRepository ruleRepository,
	AllowanceService allowanceService,
	ICurrentUserService currentUserService)
{
	public async Task ExecuteAsync(DateOnly date, TimeSpan duration, string justification)
	{
		var userId = currentUserService.UserId;

		var rule = await ruleRepository.GetAsync();
		if (rule == null)
			throw new InvalidOperationException("O usuario não possui regras.");

		var dailyGoal = rule.GetGoalForDate(date);

		allowanceService.ValidateAllowanceRequest(duration, dailyGoal);

		var allowance = new TimeAllowance(userId, date, duration, justification);
		await allowanceRepository.AddAsync(allowance);
	}
}