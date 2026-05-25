using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/workjourneyrule")]
public class WorkJourneyRuleController(
	GetWorkJourneyRuleUseCase getUseCase,
	CreateWorkJourneyRuleUseCase createUseCase,
	UpdateWorkJourneyRuleUseCase updateUseCase) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var goals = await getUseCase.ExecuteAsync();

		return Ok(goals);
	}

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