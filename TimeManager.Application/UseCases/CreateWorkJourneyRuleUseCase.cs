using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class CreateWorkJourneyRuleUseCase(IWorkJourneyRuleRepository repository, IUserRepository userRepository)
{
	public async Task ExecuteAsync(Guid userId, Dictionary<DayOfWeek, TimeSpan> goals)
	{
		var userExists = await userRepository.ExistsByIdAsync(userId);
		if (userExists)
			throw new InvalidOperationException("Usuário não encontrado.");

		var rule = new WorkJourneyRule(
			userId,
			goals.GetValueOrDefault(DayOfWeek.Monday, TimeSpan.Zero),
			goals.GetValueOrDefault(DayOfWeek.Tuesday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Wednesday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Thursday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Friday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Saturday, TimeSpan.Zero),
            goals.GetValueOrDefault(DayOfWeek.Sunday, TimeSpan.Zero)
		);

		await repository.AddAsync(rule);
	}
}