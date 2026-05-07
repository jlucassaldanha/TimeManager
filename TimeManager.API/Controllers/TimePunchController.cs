using Microsoft.AspNetCore.Mvc;
using TimeManager.API.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimePunchController(
	RegisterRealTimePunchUseCase realTimeUseCase,
	RegisterManualPunchUseCase manualUseCase,
	UpdatePunchUseCase updateUseCase,
	DeletePunchUseCase deleteUseCase) : ControllerBase
{
	[HttpPost("realtime")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] Guid userId)
	{
		try
		{
			await realTimeUseCase.ExecuteAsync(userId);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("manual")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] ManualOrUpdatePunchRequest request)
	{
		try
		{
			await manualUseCase.ExecuteAsync(request.UserId, request.DateTime, request.Type, request.Note);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("update")]
	public async Task<IActionResult> UpdatePunch([FromBody] ManualOrUpdatePunchRequest request)
	{
		try
		{
			await updateUseCase.ExecuteAsync(request.UserId, request.DateTime, request.Type, request.Note);
			return Ok(new { Message = "Ponto atualizado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("delete")]
	public async Task<IActionResult> DeletePunch([FromBody] Guid punchId, string justification)
	{
		try
		{
			await deleteUseCase.ExecuteAsync(punchId, justification);
			return Ok(new { Message = "Ponto deletado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}