using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Services;

public class DailyHoursCalculator
{
	public TimeSpan CalculateEffectiveAllowedHours(TimeSpan workedHours, TimeSpan dailyGoal, IEnumerable<TimeAllowance> allowances)
    {
        var rawAllowedHours = TimeSpan.Zero;
        foreach (var allowance in allowances)
        {
            if (!allowance.IsDeleted)
            {
                rawAllowedHours += allowance.HoursAllowed;
            }
        }

        var missingHours = dailyGoal - workedHours;
        
        if (missingHours < TimeSpan.Zero) 
            missingHours = TimeSpan.Zero;

        return rawAllowedHours > missingHours ? missingHours : rawAllowedHours;
    }

	public TimeSpan CalculateWorkedHours(IEnumerable<TimeRecord> dailyRecords)
	{
		var sortedRecords = dailyRecords.OrderBy(r => r.Timestamp).ToList();

		TimeSpan totalWorked = TimeSpan.Zero;
		DateTime? lastEntry = null;

		foreach (var record in sortedRecords)
		{
			if (record.Type == RecordType.Entry)
			{
				lastEntry = record.Timestamp;
			} else if (record.Type == RecordType.Exit && lastEntry.HasValue)
			{
				totalWorked += record.Timestamp - lastEntry.Value;

				lastEntry = null;
			}
		}

		return totalWorked;		
	}
}