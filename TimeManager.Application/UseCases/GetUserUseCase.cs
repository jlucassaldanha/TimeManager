using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class GetUserUseCase(IUserRepository userRepository)
{
	public async Task<User> ExecuteAsync(string email)
	{
		var user = await userRepository.GetByEmailAsync(email);

		if (user == null)
			throw new Exception("Usuário não encontrado.");

		return user; 
	}
}