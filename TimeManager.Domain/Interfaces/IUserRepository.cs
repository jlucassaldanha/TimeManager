using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface IUserRepository
{
	Task AddAsync(User user);
	Task<User?> GetByEmailAsync(string email);
	Task<bool> ExistsByEmailAsync(string email);
	Task<bool> ExistsByIdAsync(Guid userId);
}