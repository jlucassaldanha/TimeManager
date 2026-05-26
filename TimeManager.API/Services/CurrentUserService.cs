using System.Security.Claims;
using TimeManager.Application.Interfaces;

namespace TimeManager.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
	public Guid UserId
	{
		get
		{
			var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
				throw new UnauthorizedAccessException("Usuário não autenticado ou token inválido.");

			return userId;
		}
	}
}