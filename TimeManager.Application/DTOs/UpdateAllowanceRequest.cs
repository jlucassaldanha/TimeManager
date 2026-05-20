namespace TimeManager.Application.DTOs;

public class UpdateAllowanceRequest
{
	public required Guid Id { get; set; }
	public required TimeSpan Duration { get; set; }
	public required string Justification { get; set; }
	public required string AuditJustification { get; set; }
}