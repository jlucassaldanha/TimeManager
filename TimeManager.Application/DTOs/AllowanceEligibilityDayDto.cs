namespace TimeManager.Application.DTOs;

public class AllowanceEligibilityDayDto
{
    public DateTime Date { get; set; }
    public bool IsEligible { get; set; }
    public string? Reason { get; set; } 
}