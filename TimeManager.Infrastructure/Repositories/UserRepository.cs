using Microsoft.EntityFrameworkCore;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;
using TimeManager.Infrastructure.Data;

namespace TimeManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
	public async Task AddAsync(User user)
	{
		await context.Users.AddAsync(user);
		await context.SaveChangesAsync();
	}

	public async Task<bool> ExistsByEmailAsync(string email)
	{
		return await context.Users.AnyAsync(u => u.Email == email);
	}
}