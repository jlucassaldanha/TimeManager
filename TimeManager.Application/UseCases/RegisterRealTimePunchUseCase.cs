using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterRealTimePunchUseCase(
	ITimeRecordRepository repository,
	ICurrentUserService currentUserService)
{
	public async Task ExecuteAsync()
	{
		var userId = currentUserService.UserId;

		var now = DateTime.UtcNow;

		var todayRecords = await repository.GetRecordsByDateAsync(now.Date);
		var lastRecord = todayRecords.LastOrDefault();

		var nextType = (lastRecord == null || lastRecord.Type == RecordType.Exit) 
			? RecordType.Entry
			: RecordType.Exit;

		var newRecord = new TimeRecord(userId, now, nextType, null);
		await repository.AddAsync(newRecord);
	}
}