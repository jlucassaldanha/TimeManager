using Microsoft.AspNetCore.Mvc;
using TimeManager.API.DTOs;
using TimeManager.Application.UseCases;

namespace TimeManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(CreateUserUseCase createUserUseCase) : ControllerBase
{
	private readonly CreateUserUseCase _createUserUseCase = createUserUseCase;

	[HttpPost]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
	{	
		try
		{
			var userId = await _createUserUseCase.ExecuteAsync(request.Name, request.Email);
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