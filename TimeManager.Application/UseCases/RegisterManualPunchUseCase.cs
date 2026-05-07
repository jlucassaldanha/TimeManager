using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterManualPunchUseCase(ITimeRecordRepository repository, IUserRepository userRepository)
{
	public async Task ExecuteAsync(Guid userId, DateTime dateTime, string type, string note)
	{
		var userExists = await userRepository.ExistsByIdAsync(userId);
		if (userExists)
			throw new InvalidOperationException("Usuário não encontrado.");

		var isDuplicate = await repository.ExistsPunchAtAsync(userId, dateTime);
        if (isDuplicate)
            throw new InvalidOperationException("Já existe um ponto registrado exatamente neste horário.");
		
		if (!Enum.TryParse(type, out RecordType typeEnum))
			throw new InvalidOperationException("Tipo de ponto invalido");

		var newRecord = new TimeRecord(userId, dateTime, typeEnum, note);
		await repository.AddAsync(newRecord);
	}
}