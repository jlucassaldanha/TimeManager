namespace TimeManager.API.DTOs;

public class CreateAllowanceRequest
{
	public required Guid UserId { get; set; }
	public required DateOnly Date { get; set; }
	public required TimeSpan Duration { get; set; }
	public required string Justification { get; set; }
}