using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AllowanceController(
	CreateAllowanceUseCase createUseCase,
	GetAllowanceEligibilityUseCase getEligibilityUseCase) : ControllerBase
{
	[HttpPost("create")]
	public async Task<IActionResult> RegisterRealTimePunch([FromBody] CreateAllowanceRequest request)
	{
		try
		{
			await createUseCase.ExecuteAsync(request.UserId, request.Date, request.Duration, request.Justification);
			return Ok(new { Message = "Ponto registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
	// Atualizar
	[HttpGet("eligibility/{userId}")]
	public async Task<IActionResult> GetEligibility(Guid userId, [FromQuery] int year, [FromQuery] int month)
	{
		var eligibilityList = await getEligibilityUseCase.ExecuteAsync(userId, year, month);
    	return Ok(eligibilityList);
	}
}