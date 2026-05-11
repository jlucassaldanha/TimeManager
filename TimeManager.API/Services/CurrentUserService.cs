using System.Security.Claims;
using TimeManager.Application.Interfaces;

namespace TimeManager.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        
        if (user == null || !user.Identity!.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("Usuário não autenticado.");
        }

        var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            throw new UnauthorizedAccessException("Token inválido ou ID do usuário ausente.");
        }

        return userId;
    }
}