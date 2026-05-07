using Microsoft.AspNetCore.Mvc;
using TimeManager.API.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkJourneyRuleController(CreateWorkJourneyRuleUseCase useCase) : ControllerBase
{
    private readonly CreateWorkJourneyRuleUseCase _useCase = useCase;

    [HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateWorkJourneyRuleRequest request)
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

		await _useCase.ExecuteAsync(request.UserId, goals);
		return Ok(new { Message = "Regra de jornada registrada com sucesso"});
	}
}