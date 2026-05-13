namespace TimeManager.Application.DTOs;

public class DeletePunchRequest {
	public required Guid PunchId { get; set;}  
	public required string Justification { get; set;} 
}