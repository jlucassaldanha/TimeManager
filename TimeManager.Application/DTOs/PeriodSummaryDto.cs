namespace TimeManager.Application.DTOs;

public record PeriodSummaryDto(
	DateOnly StartDate,
	DateOnly EndDate,
	TimeSpan TotalAllowedHours,
	TimeSpan TotalWorkedHours,
	TimeSpan TotalHours,
	TimeSpan Goal,
	TimeSpan Balance,
	IReadOnlyCollection<DailySummaryDto> Days
);