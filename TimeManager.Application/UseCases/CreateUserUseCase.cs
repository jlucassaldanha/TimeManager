using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class CreateUserUseCase(IUserRepository repository)
{
	public async Task<Guid> ExecuteAsync(string name, string email)
	{
		var emailExists = await repository.ExistsByEmailAsync(email);
		if (emailExists)
			throw new InvalidOperationException("Este email já esta em uso.");

		var user = new User(name, email);

		await repository.AddAsync(user);

		return user.Id;
	}
}