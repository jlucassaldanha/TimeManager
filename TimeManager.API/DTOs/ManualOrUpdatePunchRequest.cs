using TimeManager.Domain.Entities;

namespace TimeManager.API.DTOs;

public class ManualOrUpdatePunchRequest {
	public required Guid UserId { get; set;} 
	public required DateTime DateTime { get; set;}  
	public required RecordType Type { get; set;} 
	public required string Note { get; set;}
}