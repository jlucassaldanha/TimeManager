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
	public async Task<DailySummaryDto> ExecuteAsync(Guid userId, DateTime date)
	{
		var records = await recordRepository.GetRecordsByUserIdAndDateAsync(userId, date);
		var allowance = await allowanceRepository.GetByUserIdAndDateAllowanceAsync(userId, date);
		var journeyRule = await ruleRepository.GetByUserIdAsync(userId);

		if (journeyRule == null)
			throw new InvalidOperationException("User need journey rules");

		var workedHours = calculator.Calculate(records);
		var allowedHours = allowance?.HoursAllowed ?? TimeSpan.Zero;
		var totalAccountedHours = workedHours + allowedHours;

		var balance = journeyRule.CalculateBalance(date, totalAccountedHours);
		var dailyGoal = journeyRule.GetGoalForDate(date);

		var punches = records.Select(r => new TimePunchDto(
			Id: r.Id,
			Timestamp: r.Timestamp,
			Type: r.Type.ToString()
		)).ToList();

		return new DailySummaryDto(
			Date: date,
			WorkedHours: workedHours,
			AllowedHours: allowedHours,
			TotalHours: totalAccountedHours,
			DailyGoal: dailyGoal,
			Balance: balance,
			Punches: punches
		);
	}
}