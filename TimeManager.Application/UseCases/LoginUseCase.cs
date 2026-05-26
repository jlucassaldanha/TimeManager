using TimeManager.Application.Interfaces;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class LoginUseCase(
	IUserRepository userRepository,
	IPasswordHasher passwordHasher,
	ITokenGenerator tokenGenerator)
{
	public async Task<string> ExcuteAsync(string email, string password)
	{
		var user = await userRepository.GetByEmailAsync(email);

		if (user == null || !passwordHasher.Verify(user.PasswordHash, password))
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        

		return tokenGenerator.GenerateToken(user);
	}
}