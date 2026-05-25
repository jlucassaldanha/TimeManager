using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterUserUseCase(
	IUserRepository userRepository,
	IPasswordHasher passwordHasher)
{
	public async Task ExecuteAsync(string email, string password)
	{
		var existingUser = await userRepository.GetByEmailAsync(email);
		if (existingUser != null)
			throw new InvalidOperationException("E-mail já cadastrado");

		var hashedPassword = passwordHasher.Hash(password);

		var newUser = new User(email, hashedPassword);

		await userRepository.AddAsync(newUser);
	}
}