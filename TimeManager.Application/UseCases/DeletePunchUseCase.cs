using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class DeletePunchUseCase(ITimeRecordRepository repository)
{
	public async Task ExecuteAsync(Guid recordId, string justification)
	{
		var existingRecord = await repository.GetByIdAsync(recordId);

		if (existingRecord == null)
            throw new InvalidOperationException("Registro de ponto não encontrado.");
        
		existingRecord.MarkAsDeleted(justification);
		await repository.UpdateAsync(existingRecord);
	}
}