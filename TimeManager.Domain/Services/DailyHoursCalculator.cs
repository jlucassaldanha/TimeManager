using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Services;

public class DailyHoursCalculator
{
	public TimeSpan Calculate(IEnumerable<TimeRecord> dailyRecords)
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