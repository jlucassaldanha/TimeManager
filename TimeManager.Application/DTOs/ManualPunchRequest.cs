namespace TimeManager.Application.DTOs;

public class ManualPunchRequest {
	public required DateTime DateTime { get; set;}  
	public required string Type { get; set;} 
	public required string Note { get; set;}
}