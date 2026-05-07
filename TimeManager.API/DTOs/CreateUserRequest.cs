namespace TimeManager.API.DTOs;

public class CreateUserRequest
{
	public required string Name { get; set; }
	public required string Email { get; set; }
}