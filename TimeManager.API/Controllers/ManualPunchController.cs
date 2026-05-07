using Microsoft.AspNetCore.Mvc;
using TimeManager.API.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManualPunchController(RegisterManualPunchUseCase useCase) : ControllerBase
{
	[HttpPost("realtime")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] ManualPunchRequest request)
	{
		try
		{
			await useCase.ExecuteAsync(request.UserId, request.DateTime, request.Type, request.Note);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}