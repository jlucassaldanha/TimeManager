using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class UpdateWorkJourneyRuleUseCase(IWorkJourneyRuleRepository repository)
{
	public async Task ExecuteAsync(Guid userId, Dictionary<DayOfWeek, TimeSpan> goals)
	{
		var existingRule = await repository.GetByUserIdAsync(userId);

		if (existingRule == null)
            throw new InvalidOperationException("Regra não encontrada.");

		existingRule.UpdateDetails(
			goals.GetValueOrDefault(DayOfWeek.Monday, TimeSpan.Zero),
			goals.GetValueOrDefault(DayOfWeek.Tuesday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Wednesday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Thursday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Friday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Saturday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Sunday, TimeSpan.Zero)
		);

		await repository.UpdateAsync(existingRule);
	}
}