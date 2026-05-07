using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class CreateWorkJourneyRuleUseCase(IWorkJourneyRuleRepository repository)
{
	public readonly IWorkJourneyRuleRepository _repository = repository;

	public async Task ExecuteAsync(Guid userId, Dictionary<DayOfWeek, TimeSpan> goals)
	{
		var rule = new WorkJourneyRule(
			userId,
			goals[DayOfWeek.Monday],
			goals[DayOfWeek.Tuesday],
            goals[DayOfWeek.Wednesday],
            goals[DayOfWeek.Thursday],
            goals[DayOfWeek.Friday],
            goals[DayOfWeek.Saturday],
            goals[DayOfWeek.Sunday]
		);

		await _repository.AddAsync(rule);
	}
}