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
	private readonly ITimeRecordRepository _recordRepository = recordRepository;
    private readonly ITimeAllowanceRepository _allowanceRepository = allowanceRepository;
    private readonly IWorkJourneyRuleRepository _ruleRepository = ruleRepository;
    private readonly DailyHoursCalculator _calculator = calculator;

	public async Task<DailySummaryDto> ExecuteAsync(Guid userId, DateTime date)
	{
		var records = await _recordRepository.GetRecordsByUserIdAndDateAsync(userId, date);
		var allowance = await _allowanceRepository.GetValidAllowanceAsync(userId, date);
		var journeyRule = await _ruleRepository.GetByUserIdAsync(userId);

		if (journeyRule == null)
			throw new InvalidOperationException("User need journey rules");

		var workedHours = _calculator.Calculate(records);
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