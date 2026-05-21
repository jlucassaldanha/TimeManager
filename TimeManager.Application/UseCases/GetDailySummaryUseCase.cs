using TimeManager.Application.DTOs;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;

namespace TimeManager.Application.UseCases;

public class GetDailySummaryUseCase(
        ITimeRecordRepository recordRepository,
        ITimeAllowanceRepository allowanceRepository,
        IWorkJourneyRuleRepository ruleRepository,
        DailyHoursCalculator calculator)
{
	public async Task<DailySummaryDto?> ExecuteAsync(DateTime date)
	{
		var dateOnly = DateOnly.FromDateTime(date);

		var records = await recordRepository.GetRecordsByDateAsync(date);
		var allowances = await allowanceRepository.GetByDateAllowancesAsync(dateOnly);
		var journeyRule = await ruleRepository.GetAsync();

		if (!records.Any() && !allowances.Any()) return null;

		if (journeyRule == null)
			throw new InvalidOperationException("User need journey rules");
			
		var dailyGoal = journeyRule.GetGoalForDate(dateOnly);

		var workedHours = calculator.CalculateWorkedHours(records);
		var allowedHours = calculator.CalculateEffectiveAllowedHours(workedHours, dailyGoal, allowances);
		
		var totalAccountedHours = workedHours + allowedHours;
		var balance = journeyRule.CalculateBalance(dateOnly, totalAccountedHours);
		var balancePostTolerance = calculator.GetBalancePostTolerance(balance);

		var punches = records.Select(r => new TimePunchDto(
			Id: r.Id,
			Timestamp: r.Timestamp,
			Type: r.Type.ToString(),
			Note: r.Note
		)).ToList();

		return new DailySummaryDto(
			Date: dateOnly,
			WorkedMinutes: (int)workedHours.TotalMinutes,
			AllowedMinutes: (int)allowedHours.TotalMinutes,
			TotalMinutes: (int)totalAccountedHours.TotalMinutes,
			DailyGoalMinutes: (int)dailyGoal.TotalMinutes,
			BalanceMinutes: (int)balancePostTolerance.TotalMinutes,
			Punches: punches
		);
	}
}