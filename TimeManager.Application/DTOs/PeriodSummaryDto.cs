namespace TimeManager.Application.DTOs;

public record PeriodSummaryDto(
	DateOnly StartDate,
	DateOnly EndDate,
	int TotalAllowedMinutes,
	int TotalWorkedMinutes,
	int TotalMinutes,
	int GoalMinutes,
	int BalanceMinutes,
	IReadOnlyCollection<DailySummaryDto> Days
);
