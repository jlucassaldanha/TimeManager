using Microsoft.EntityFrameworkCore;
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

	public async Task<bool> ExistsByEmailAsync(string email)
	{
		return await _context.Users.AnyAsync(u => u.Email == email);
	}
}