using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterRealTimePunchUseCase(ITimeRecordRepository repository)
{
	private readonly ITimeRecordRepository _repository = repository;

	public async Task ExecuteAsync(Guid userId)
	{
		var now = DateTime.UtcNow;

		var todayRecords = await _repository.GetRecordsByUserIdAndDateAsync(userId, now.Date);
		var lastRecord = todayRecords.OrderBy(r => r.Timestamp).FirstOrDefault();

		var nextType = (lastRecord == null || lastRecord.Type == RecordType.Exit) 
			? RecordType.Entry
			: RecordType.Exit;

		var newRecord = new TimeRecord(userId, now, nextType, null);
		await _repository.AddAsync(newRecord);
	}
}