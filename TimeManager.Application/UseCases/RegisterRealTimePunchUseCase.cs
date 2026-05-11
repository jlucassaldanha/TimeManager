using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterRealTimePunchUseCase(ITimeRecordRepository repository, ICurrentUserService userService)
{
	public async Task ExecuteAsync()
	{
		var userId = userService.GetUserId();

		var now = DateTime.UtcNow;

		var todayRecords = await repository.GetRecordsByUserIdAndDateAsync(userId, now.Date);
		var lastRecord = todayRecords.LastOrDefault();

		var nextType = (lastRecord == null || lastRecord.Type == RecordType.Exit) 
			? RecordType.Entry
			: RecordType.Exit;

		var newRecord = new TimeRecord(userId, now, nextType, null);
		await repository.AddAsync(newRecord);
	}
}