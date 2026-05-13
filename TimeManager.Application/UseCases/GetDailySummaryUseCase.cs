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
		var allowances = await allowanceRepository.GetByDateAllowanceAsync(dateOnly);
		var journeyRule = await ruleRepository.GetAsync();

		if (!records.Any() && !allowances.Any()) return null;

		if (journeyRule == null)
			throw new InvalidOperationException("User need journey rules");
			
		var dailyGoal = journeyRule.GetGoalForDate(dateOnly);

		var workedHours = calculator.CalculateWorkedHours(records);
		var allowedHours = calculator.CalculateEffectiveAllowedHours(workedHours, dailyGoal, allowances);
		
		var totalAccountedHours = workedHours + allowedHours;
		var balance = journeyRule.CalculateBalance(dateOnly, totalAccountedHours);

		var punches = records.Select(r => new TimePunchDto(
			Id: r.Id,
			Timestamp: r.Timestamp,
			Type: r.Type.ToString()
		)).ToList();

		return new DailySummaryDto(
			Date: dateOnly,
			WorkedHours: workedHours,
			AllowedHours: allowedHours,
			TotalHours: totalAccountedHours,
			DailyGoal: dailyGoal,
			Balance: balance,
			Punches: punches
		);
	}
}