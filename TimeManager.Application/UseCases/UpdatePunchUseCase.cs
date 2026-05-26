using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class UpdatePunchUseCase(ITimeRecordRepository repository)
{
	public async Task ExecuteAsync(Guid recordId, DateTime newDateTime, string newType, string newNote)
	{
		var existingRecord = await repository.GetByIdAsync(recordId);

		if (existingRecord == null)
            throw new InvalidOperationException("Registro de ponto não encontrado.");

		if (!Enum.TryParse(newType, out RecordType newTypeEnum))
			throw new InvalidOperationException("Tipo de ponto invalido");

		existingRecord.UpdateDetails(newDateTime, newTypeEnum, newNote);
		await repository.UpdateAsync(existingRecord);
	}
}