using TimeManager.Application.DTOs;
using TimeManager.Domain.Interfaces;
using TimeManager.Domain.Services;

namespace TimeManager.Application.UseCases;

public class GetAllowanceEligibilityUseCase(
	IWorkJourneyRuleRepository ruleRepository,
	ITimeRecordRepository recordRepository,
	DailyHoursCalculator calculator)
{
	public async Task<List<AllowanceEligibilityDayDto>> ExecuteAsync(int year, int month)
	{
		var result = new List<AllowanceEligibilityDayDto>();

		var rule = await ruleRepository.GetAsync();
		if (rule == null) return result;

		var daysInMonth = DateTime.DaysInMonth(year, month);
		
		for (int day = 1; day <= daysInMonth; day++)
		{
			var currentDate = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
			var dailyGoal = rule.GetGoalForDate(DateOnly.FromDateTime(currentDate));

			var dayRecords = await recordRepository.GetRecordsByDateAsync(currentDate);
			var totalWorkedHours = calculator.CalculateWorkedHours(dayRecords);

			var dayDto = new AllowanceEligibilityDayDto { Date = currentDate };

			if (dailyGoal == TimeSpan.Zero)
			{
				dayDto.IsEligible = false;
				dayDto.Reason = "Sem jornada prevista.";
			}
			else if (totalWorkedHours >= dailyGoal)
			{
				dayDto.IsEligible = false;
				dayDto.Reason = "Jornada cumprida.";
			}
			else
			{
				dayDto.IsEligible = true;
                dayDto.Reason = "Abono permitido.";
			}

			result.Add(dayDto);
		}

		return result;
	}
}