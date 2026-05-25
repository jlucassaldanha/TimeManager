using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);
public record LoginResponse(string Token);

[ApiController]
[Route("api/auth")]
public class AuthController(
	RegisterUserUseCase registerUseCase,
	LoginUseCase loginUseCase) : ControllerBase
{
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request)
	{
		try
		{
			await registerUseCase.ExecuteAsync(request.Email, request.Password);
			return Created();
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch(Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		try
		{
			var token = await loginUseCase.ExcuteAsync(request.Email, request.Password);
			return Ok(new LoginResponse(token));
		}
		catch (UnauthorizedAccessException ex)
		{
			return Unauthorized(new { message = ex.Message });
		}
		catch(Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}