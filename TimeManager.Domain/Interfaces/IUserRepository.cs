using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Interfaces;

public interface IUserRepository
{
	Task AddAsync(User user);
}