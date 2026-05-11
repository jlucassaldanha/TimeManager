namespace TimeManager.Application.DTOs;

public class CreateOrUpdateWorkJourneyRuleRequest
{
    public Guid UserId { get; set; }
    public string Monday { get; set; } = "08:00:00";
    public string Tuesday { get; set; } = "08:00:00";
    public string Wednesday { get; set; } = "08:00:00";
    public string Thursday { get; set; } = "08:00:00";
    public string Friday { get; set; } = "08:00:00";
    public string Saturday { get; set; } = "00:00:00";
    public string Sunday { get; set; } = "00:00:00";
}