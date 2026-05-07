using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimePunchController(RegisterRealTimePunchUseCase registerUseCase) : ControllerBase
{
	private readonly RegisterRealTimePunchUseCase _registerUseCase = registerUseCase;

	[HttpPost("realtime")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] Guid userId)
	{
		try
		{
			await _registerUseCase.ExecuteAsync(userId);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}