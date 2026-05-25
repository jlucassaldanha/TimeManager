namespace TimeManager.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
}