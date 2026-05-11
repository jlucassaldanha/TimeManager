using TimeManager.Domain.Entities;

namespace TimeManager.Application.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task RegisterAsync(User user, string password);
}