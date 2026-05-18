namespace TimeManager.Application.DTOs;

public record WorkJourneyRuleDto
(
    Guid Id,
    string Monday,
    string Tuesday,
    string Wednesday,
    string Thursday,
    string Friday,
    string Saturday,
    string Sunday
);