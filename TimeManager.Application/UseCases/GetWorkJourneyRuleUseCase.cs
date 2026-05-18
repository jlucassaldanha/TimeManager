using TimeManager.Application.DTOs;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class GetWorkJourneyRuleUseCase(IWorkJourneyRuleRepository repository)
{
	public async Task<WorkJourneyRuleDto> ExecuteAsync()
	{

		var existingRule = await repository.GetAsync();

		if (existingRule == null)
            throw new InvalidOperationException("Regra não encontrada.");

		return new WorkJourneyRuleDto(
			existingRule.Id,
			existingRule.MondayGoal.ToString(@"hh\:mm"),
			existingRule.TuesdayGoal.ToString(@"hh\:mm"),
			existingRule.WednesdayGoal.ToString(@"hh\:mm"),
			existingRule.ThursdayGoal.ToString(@"hh\:mm"),
			existingRule.FridayGoal.ToString(@"hh\:mm"),
			existingRule.SaturdayGoal.ToString(@"hh\:mm"),
			existingRule.SundayGoal.ToString(@"hh\:mm")	
		);
	}
}