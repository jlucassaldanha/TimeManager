using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(CreateUserUseCase createUseCase, GetUserUseCase getUseCase) : ControllerBase
{
	[HttpGet("{email}")]
	public async Task<IActionResult> GetUser(string email)
	{	
		try
		{
			var user = await getUseCase.ExecuteAsync(email);
			return Ok(new { UserId = user.Id, UserName = user.Name });
		}
		catch (Exception ex)
		{
			return BadRequest(new { Error = ex.Message} );
		}
	}

	[HttpPost("create")]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
	{	
		try
		{
			var userId = await createUseCase.ExecuteAsync(request.Name, request.Email);
			return Ok(new { Message = "Usuário criado!", UserId = userId });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { Error = ex.Message} );
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}
}