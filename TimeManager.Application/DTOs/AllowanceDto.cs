namespace TimeManager.Application.DTOs;

public record AllowanceDto(
	Guid Id,
	DateOnly Date,
	string Duration,
	string Justification
);
