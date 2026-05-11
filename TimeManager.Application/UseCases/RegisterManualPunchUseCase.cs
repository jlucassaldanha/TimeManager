using TimeManager.Application.DTOs;
using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class RegisterManualPunchUseCase(ITimeRecordRepository repository, ICurrentUserService userService)
{
	public async Task ExecuteAsync(ManualPunchRequest request)
	{
		var userId = userService.GetUserId();

		var isDuplicate = await repository.ExistsPunchAtAsync(userId, request.DateTime);
        if (isDuplicate)
            throw new InvalidOperationException("Já existe um ponto registrado exatamente neste horário.");
		
		if (!Enum.TryParse(request.Type, out RecordType typeEnum))
			throw new InvalidOperationException("Tipo de ponto invalido");

		var newRecord = new TimeRecord(userId, request.DateTime, typeEnum, request.Note);
		await repository.AddAsync(newRecord);
	}
}