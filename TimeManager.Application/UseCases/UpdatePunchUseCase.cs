using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class UpdatePunchUseCase(ITimeRecordRepository repository)
{
	public async Task ExecuteAsync(Guid recordId, DateTime newDateTime, RecordType newType, string newNote)
	{
		var existingRecord = await repository.GetByIdAsync(recordId);

		if (existingRecord == null)
            throw new InvalidOperationException("Registro de ponto não encontrado.");
        
		existingRecord.UpdateDetails(newDateTime, newType, newNote);
		await repository.UpdateAsync(existingRecord);
	}
}