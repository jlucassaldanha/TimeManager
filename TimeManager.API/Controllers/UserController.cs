using Microsoft.AspNetCore.Mvc;
using TimeManager.Application.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(CreateUserUseCase useCase) : ControllerBase
{
	[HttpPost("create")]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
	{	
		try
		{
			var userId = await useCase.ExecuteAsync(request.Name, request.Email);
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