namespace TimeManager.Application.DTOs;

public class DeleteAllowanceRequest
{
	public required Guid Id { get; set; }
	public required string AuditJustification { get; set; }
}