using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/allowance")]
public class AllowanceController(
	CreateAllowanceUseCase createUseCase,
	GetAllowanceEligibilityUseCase getEligibilityUseCase,
	UpdateAllowanceUseCase updateUseCase,
	DeleteAllowanceUseCase deleteUseCase,
	GetAllowanceUseCase getAllowanceUseCase) : ControllerBase
{

	[HttpGet]
	public async Task<IActionResult> GetAllowanceByDate([FromQuery] DateOnly date)
	{
		var allowance = await getAllowanceUseCase.ExecuteAsync(date);
		return Ok(allowance);
	}

	[HttpPost("create")]
	public async Task<IActionResult> RegisterAllowance([FromBody] CreateAllowanceRequest request)
	{
		try
		{
			await createUseCase.ExecuteAsync(request.Date, request.Duration, request.Justification);
			return Ok(new { Message = "Abono registrado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("update")]
	public async Task<IActionResult> UpdateAllowance([FromBody] UpdateAllowanceRequest request)
	{
		try
		{
			await updateUseCase.ExecuteAsync(request.Id, request.Duration, request.Justification, request.AuditJustification);
			return Ok(new { Message = "Abono atualizado com sucesso."});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("delete")]
	public async Task<IActionResult> DeleteAllowance([FromBody] DeleteAllowanceRequest request)
	{
		try
		{
			await deleteUseCase.ExecuteAsync(request.Id, request.AuditJustification);
			return Ok(new { Message = "Ponto deletado com sucesso."});
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