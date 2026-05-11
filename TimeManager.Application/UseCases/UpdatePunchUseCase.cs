using TimeManager.Application.DTOs;
using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class UpdatePunchUseCase(ITimeRecordRepository repository, ICurrentUserService userService)
{
	public async Task ExecuteAsync(UpdatePunchRequest request)
	{
		var userId = userService.GetUserId();

		var existingRecord = await repository.GetByIdAndUserIdAsync(request.RecordId, userId);

		if (existingRecord == null)
            throw new InvalidOperationException("Registro de ponto não encontrado.");

		if (!Enum.TryParse(request.Type, out RecordType newTypeEnum))
			throw new InvalidOperationException("Tipo de ponto invalido");

		existingRecord.UpdateDetails(request.DateTime, newTypeEnum, request.Note);
		await repository.UpdateAsync(existingRecord);
	}
}