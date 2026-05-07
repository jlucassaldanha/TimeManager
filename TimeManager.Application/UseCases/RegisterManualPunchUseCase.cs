using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterManualPunchUseCase(ITimeRecordRepository repository)
{
	public async Task ExecuteAsync(Guid userId, DateTime dateTime, RecordType type, string note)
	{
		var newRecord = new TimeRecord(userId, dateTime, type, note);
		await repository.AddAsync(newRecord);
	}
}