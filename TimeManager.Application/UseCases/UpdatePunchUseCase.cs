using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class UpdatePunchUseCase(ITimeRecordRepository repository, IUserRepository userRepository)
{
	public async Task ExecuteAsync(Guid userId, Guid recordId, DateTime newDateTime, string newType, string newNote)
	{
		var userExists = await userRepository.ExistsByIdAsync(userId);
		if (userExists)
			throw new InvalidOperationException("Usuário não encontrado.");

		var existingRecord = await repository.GetByIdAsync(recordId);

		if (existingRecord == null)
            throw new InvalidOperationException("Registro de ponto não encontrado.");
        
		var isDuplicate = await repository.ExistsPunchAtAsync(userId, newDateTime);
        if (isDuplicate)
            throw new InvalidOperationException("Já existe um ponto registrado exatamente neste horário.");

		if (!Enum.TryParse(newType, out RecordType newTypeEnum))
			throw new InvalidOperationException("Tipo de ponto invalido");

		existingRecord.UpdateDetails(newDateTime, newTypeEnum, newNote);
		await repository.UpdateAsync(existingRecord);
	}
}