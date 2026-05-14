namespace TimeManager.Application.DTOs;

public record DailySummaryDto(
	DateOnly Date,
	int WorkedMinutes,
	int AllowedMinutes,
	int TotalMinutes,
	int DailyGoalMinutes,
	int BalanceMinutes,
	IReadOnlyCollection<TimePunchDto> Punches
);
