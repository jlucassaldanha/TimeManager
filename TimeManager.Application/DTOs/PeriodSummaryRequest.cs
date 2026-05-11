namespace TimeManager.Application.DTOs;

public record PeriodSummaryRequest(Guid UserId, DateTime StartDate, DateTime EndDate);