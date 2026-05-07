using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DailySummaryUseCase(GetDailySummaryUseCase useCase) : ControllerBase
{
	[HttpGet("{userId}/{date}")]
	public async Task<IActionResult> GetSummary(Guid userId, DateTime date)
	{
		try
		{
			var utcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);

			var summary = await useCase.ExecuteAsync(userId, utcDate);

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