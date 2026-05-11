using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AllowanceController(
	CreateAllowanceUseCase createUseCase,
	GetAllowanceEligibilityUseCase getEligibilityUseCase) : ControllerBase
{
	[HttpPost("create")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] CreateAllowanceRequest request)
	{
		try
		{
			await createUseCase.ExecuteAsync(request);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpGet("eligibility")]
	public async Task<IActionResult> GetEligibility([FromQuery] int year, [FromQuery] int month)
	{
		var eligibilityList = await getEligibilityUseCase.ExecuteAsync(year, month);
    	return Ok(eligibilityList);
	}
}