using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
	private readonly AppDbContext _context = context;

	public async Task AddAsync(User user)
	{
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}
}