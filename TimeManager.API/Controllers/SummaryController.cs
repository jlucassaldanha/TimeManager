using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SummaryController(GetDailySummaryUseCase dailyUseCase, GetPeriodSummaryUseCase periodUseCase) : ControllerBase
{
	[HttpGet("daily")]
	public async Task<IActionResult> GetDailySummary([FromQuery] Guid userId, [FromQuery] DateTime date)
	{
		try
		{
			var utcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);

			var summary = await dailyUseCase.ExecuteAsync(userId, utcDate);

			if (summary == null)
				return NotFound(new { Message = "Nenhum registro encontrado"});

			return Ok(summary);
		}
		catch (Exception ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpGet("period")]
	public async Task<IActionResult> GetPeriodSummary([FromQuery] PeriodSummaryRequest request)
	{
		try
		{
			var utcStartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
			var utcEndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

			var summary = await periodUseCase.ExecuteAsync(request);

			if (summary == null)
				return NotFound(new { Message = "Nenhum registro encontrado"});

			return Ok(summary);
		}
		catch (Exception ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}