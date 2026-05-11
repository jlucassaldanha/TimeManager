namespace TimeManager.Application.DTOs;

public class CreateAllowanceRequest
{
	public required DateOnly Date { get; set; }
	public required TimeSpan Duration { get; set; }
	public required string Justification { get; set; }
}