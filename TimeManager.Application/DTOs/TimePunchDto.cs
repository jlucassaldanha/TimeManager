namespace TimeManager.Application.DTOs;

public record TimePunchDto(
	Guid Id,
	DateTime Timestamp,
	string Type
);