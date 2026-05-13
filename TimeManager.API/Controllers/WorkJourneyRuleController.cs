using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkJourneyRuleController(
	CreateWorkJourneyRuleUseCase createUseCase,
	UpdateWorkJourneyRuleUseCase updateUseCase) : ControllerBase
{
    [HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] CreateOrUpdateWorkJourneyRuleRequest request)
	{
		var goals = new Dictionary<DayOfWeek, TimeSpan>
		{
			{ DayOfWeek.Monday, TimeSpan.Parse(request.Monday) },
			{ DayOfWeek.Tuesday, TimeSpan.Parse(request.Tuesday) },
            { DayOfWeek.Wednesday, TimeSpan.Parse(request.Wednesday) },
            { DayOfWeek.Thursday, TimeSpan.Parse(request.Thursday) },
            { DayOfWeek.Friday, TimeSpan.Parse(request.Friday) },
            { DayOfWeek.Saturday, TimeSpan.Parse(request.Saturday) },
            { DayOfWeek.Sunday, TimeSpan.Parse(request.Sunday) }
		};

		await createUseCase.ExecuteAsync(goals);
		return Ok(new { Message = "Regra de jornada registrada com sucesso"});
	}

	[HttpPost("update")]
	public async Task<IActionResult> Update([FromBody] CreateOrUpdateWorkJourneyRuleRequest request)
	{
		var goals = new Dictionary<DayOfWeek, TimeSpan>
		{
			{ DayOfWeek.Monday, TimeSpan.Parse(request.Monday) },
			{ DayOfWeek.Tuesday, TimeSpan.Parse(request.Tuesday) },
            { DayOfWeek.Wednesday, TimeSpan.Parse(request.Wednesday) },
            { DayOfWeek.Thursday, TimeSpan.Parse(request.Thursday) },
            { DayOfWeek.Friday, TimeSpan.Parse(request.Friday) },
            { DayOfWeek.Saturday, TimeSpan.Parse(request.Saturday) },
            { DayOfWeek.Sunday, TimeSpan.Parse(request.Sunday) }
		};

		await updateUseCase.ExecuteAsync(goals);
		return Ok(new { Message = "Regra de jornada registrada com sucesso"});
	}
}