using TimeManager.Domain.Entities;

namespace TimeManager.Application.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}