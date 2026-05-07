using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimePunchController(RegisterRealTimePunchUseCase useCase) : ControllerBase
{
	[HttpPost("realtime")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] Guid userId)
	{
		try
		{
			await useCase.ExecuteAsync(userId);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}