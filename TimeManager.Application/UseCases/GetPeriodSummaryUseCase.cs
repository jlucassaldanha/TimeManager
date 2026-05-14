using TimeManager.Application.DTOs;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;

public class GetPeriodSummaryUseCase(
	ITimeRecordRepository recordRepository, 
	ITimeAllowanceRepository allowanceRepository,
	IWorkJourneyRuleRepository ruleRepository,
	DailyHoursCalculator calculator)
{
    public async Task<PeriodSummaryDto?> ExecuteAsync(PeriodSummaryRequest request)
    {
		var startDateOnly = DateOnly.FromDateTime(request.StartDate);
		var endDateOnly = DateOnly.FromDateTime(request.EndDate);
	
	    if (request.StartDate > request.EndDate)
        {
            throw new ArgumentException("A data de início não pode ser maior que a data final.");
        }

        var totalDays = endDateOnly.DayNumber - startDateOnly.DayNumber;
        if (totalDays > 31)
        {
            throw new ArgumentException("O período máximo permitido para consulta é de 31 dias.");
        }

		var journeyRule = await ruleRepository.GetAsync();

		var allRecords = await recordRepository.GetByPeriodAsync(request.StartDate, request.EndDate);
		var allAllowances = await allowanceRepository.GetByPeriodAsync(startDateOnly, endDateOnly);

		if (!allRecords.Any() && !allAllowances.Any()) return null;

		if (journeyRule == null)
			throw new InvalidOperationException("User need journey rules");

		var dailySummaries = new List<DailySummaryDto>();
		var currentDate = startDateOnly;

		var totalBalance = TimeSpan.Zero;
		var totalHours = TimeSpan.Zero;
		var totalAllowedHours = TimeSpan.Zero;
		var totalWorkedHours = TimeSpan.Zero;
		var goal = TimeSpan.Zero;

		while (currentDate <= endDateOnly)
		{
			var dailyGoal = journeyRule.GetGoalForDate(currentDate);

			var recordsForDay = allRecords
                .Where(r => DateOnly.FromDateTime(r.Timestamp) == currentDate)
                .OrderBy(r => r.Timestamp)
                .ToList();

            var allowancesForDay = allAllowances
                .Where(a => a.Date == currentDate)
                .ToList();

			var workedHours = calculator.CalculateWorkedHours(recordsForDay);
			var allowedHours = calculator.CalculateEffectiveAllowedHours(workedHours, dailyGoal, allowancesForDay);
			
			var totalAccountedHours = workedHours + allowedHours;
			var balance = journeyRule.CalculateBalance(currentDate, totalAccountedHours);
			var balancePostTolerance = calculator.GetBalancePostTolerance(balance);

			var punches = recordsForDay.Select(r => new TimePunchDto(
				Id: r.Id,
				Timestamp: r.Timestamp,
				Type: r.Type.ToString()
			)).ToList();

			dailySummaries.Add(new DailySummaryDto(
				Date: currentDate,
				WorkedMinutes: (int)workedHours.TotalMinutes,
				AllowedMinutes: (int)allowedHours.TotalMinutes,
				TotalMinutes: (int)totalAccountedHours.TotalMinutes,
				DailyGoalMinutes: (int)dailyGoal.TotalMinutes,
				BalanceMinutes: (int)balancePostTolerance.TotalMinutes,
				Punches: punches
			));

			totalBalance += balancePostTolerance;
			totalHours += totalAccountedHours;
			totalWorkedHours += workedHours;
			totalAllowedHours += allowedHours;
			goal += dailyGoal;

			currentDate = currentDate.AddDays(1);
		}
        
        return new PeriodSummaryDto(
			StartDate: startDateOnly,
			EndDate: endDateOnly,
			TotalAllowedMinutes: (int)totalAllowedHours.TotalMinutes,
			TotalWorkedMinutes: (int)totalWorkedHours.TotalMinutes,
			TotalMinutes: (int)totalHours.TotalMinutes,
			GoalMinutes: (int)goal.TotalMinutes,
			BalanceMinutes: (int)totalBalance.TotalMinutes,
			dailySummaries
		);
    }
}