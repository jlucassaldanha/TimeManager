using TimeManager.Application.Interfaces;

namespace TimeManager.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
	public string Hash(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}

	public bool Verify(string hash, string providedPassword)
	{
		return BCrypt.Net.BCrypt.Verify(providedPassword, hash);
	}
}