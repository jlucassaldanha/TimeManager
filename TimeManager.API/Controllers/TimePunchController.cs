using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/timepunch")]
public class TimePunchController(
	RegisterRealTimePunchUseCase realTimeUseCase,
	RegisterManualPunchUseCase manualUseCase,
	UpdatePunchUseCase updateUseCase,
	DeletePunchUseCase deleteUseCase) : ControllerBase
{
	[HttpPost("realtime")]
	public async Task<IActionResult> RegisterRealTimePunch()
	{
		try
		{
			await realTimeUseCase.ExecuteAsync();
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("manual")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] ManualPunchRequest request)
	{
		try
		{
			await manualUseCase.ExecuteAsync(request.DateTime, request.Type, request.Note);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("update")]
	public async Task<IActionResult> UpdatePunch([FromBody] UpdatePunchRequest request)
	{
		try
		{
			await updateUseCase.ExecuteAsync(request.RecordId, request.DateTime, request.Type, request.Note);
			return Ok(new { Message = "Ponto atualizado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("delete")]
	public async Task<IActionResult> DeletePunch([FromBody] DeletePunchRequest request)
	{
		try
		{
			await deleteUseCase.ExecuteAsync(request.PunchId, request.Justification);
			return Ok(new { Message = "Ponto deletado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}