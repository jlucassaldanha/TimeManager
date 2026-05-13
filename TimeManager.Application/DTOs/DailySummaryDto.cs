namespace TimeManager.Application.DTOs;

public record DailySummaryDto(
	DateOnly Date,
	TimeSpan WorkedHours,
	TimeSpan AllowedHours,
	TimeSpan TotalHours,
	TimeSpan DailyGoal,
	TimeSpan Balance,
	IReadOnlyCollection<TimePunchDto> Punches
);
