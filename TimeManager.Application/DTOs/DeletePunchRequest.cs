namespace TimeManager.Application.DTOs;

public class DeletePunchRequest {
	public required Guid RecordId { get; set;}  
	public required string Justification { get; set;} 
}