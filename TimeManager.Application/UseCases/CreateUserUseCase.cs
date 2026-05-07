using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class CreateUserUseCase(IUserRepository repository)
{
	private readonly IUserRepository _repository = repository;

	public async Task<Guid> ExecuteAsync(string name, string email)
	{
		var user = new User(name, email);

		await _repository.AddAsync(user);

		return user.Id;
	}
}