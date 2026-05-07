using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterManualPunchUseCase(ITimeRecordRepository repository)
{
	public async Task ExecuteAsync(Guid userId, DateTime dateTime, RecordType type, string note)
	{
		var isDuplicate = await repository.ExistsPunchAtAsync(userId, dateTime);
        if (isDuplicate)
            throw new InvalidOperationException("Já existe um ponto registrado exatamente neste horário.");
			
		var newRecord = new TimeRecord(userId, dateTime, type, note);
		await repository.AddAsync(newRecord);
	}
}