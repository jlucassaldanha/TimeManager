using TimeManager.Application.DTOs;
using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;

namespace TimeManager.Application.UseCases;

public class CreateAllowanceUseCase(
	ITimeAllowanceRepository allowanceRepository,
	IWorkJourneyRuleRepository ruleRepository,
	AllowanceService allowanceService,
	ICurrentUserService userService)
{
	public async Task ExecuteAsync(CreateAllowanceRequest request)
	{
		var userId = userService.GetUserId();

		var rule = await ruleRepository.GetByUserIdAsync(userId);
		if (rule == null)
			throw new InvalidOperationException("O usuario não possui regras.");

		var dailyGoal = rule.GetGoalForDate(request.Date);

		allowanceService.ValidateAllowanceRequest(request.Duration, dailyGoal);

		var allowance = new TimeAllowance(userId, request.Date, request.Duration, request.Justification);
		await allowanceRepository.AddAsync(allowance);
	}
}