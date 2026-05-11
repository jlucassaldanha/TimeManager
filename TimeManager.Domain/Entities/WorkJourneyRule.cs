namespace TimeManager.Domain.Entities;

public class WorkJourneyRule
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }
	public TimeSpan MondayGoal { get; private set; }
    public TimeSpan TuesdayGoal { get; private set; }
    public TimeSpan WednesdayGoal { get; private set; }
    public TimeSpan ThursdayGoal { get; private set; }
    public TimeSpan FridayGoal { get; private set; }
    public TimeSpan SaturdayGoal { get; private set; }
    public TimeSpan SundayGoal { get; private set; }

	protected WorkJourneyRule() { }

    public WorkJourneyRule(Guid userId, 
        TimeSpan monday, TimeSpan tuesday, TimeSpan wednesday, 
        TimeSpan thursday, TimeSpan friday, TimeSpan saturday, TimeSpan sunday)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        MondayGoal = monday;
        TuesdayGoal = tuesday;
        WednesdayGoal = wednesday;
        ThursdayGoal = thursday;
        FridayGoal = friday;
        SaturdayGoal = saturday;
        SundayGoal = sunday;
    }

	public TimeSpan GetGoalForDate(DateOnly date)
	{
		return date.DayOfWeek switch
		{
			DayOfWeek.Monday => MondayGoal,
			DayOfWeek.Tuesday => TuesdayGoal,
            DayOfWeek.Wednesday => WednesdayGoal,
            DayOfWeek.Thursday => ThursdayGoal,
            DayOfWeek.Friday => FridayGoal,
            DayOfWeek.Saturday => SaturdayGoal,
            DayOfWeek.Sunday => SundayGoal,
            _ => TimeSpan.Zero
		};
	}

	public TimeSpan CalculateBalance(DateOnly date, TimeSpan totalWorked)
	{
		var dailyGoal = GetGoalForDate(date);
		return totalWorked - dailyGoal;
	}

    public void UpdateDetails(
        TimeSpan monday, TimeSpan tuesday, TimeSpan wednesday, 
        TimeSpan thursday, TimeSpan friday, TimeSpan saturday, TimeSpan sunday)
    {
        MondayGoal = monday;
        TuesdayGoal = tuesday;
        WednesdayGoal = wednesday;
        ThursdayGoal = thursday;
        FridayGoal = friday;
        SaturdayGoal = saturday;
        SundayGoal = sunday;
    }
}