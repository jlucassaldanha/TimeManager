using TimeManager.Application.Interfaces;
using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class DeletePunchUseCase(ITimeRecordRepository repository, ICurrentUserService userService)
{
	public async Task ExecuteAsync(Guid recordId, string justification)
	{
		var userId = userService.GetUserId();

		var existingRecord = await repository.GetByIdAndUserIdAsync(recordId, userId);

		if (existingRecord == null)
            throw new InvalidOperationException("Registro de ponto não encontrado.");
        
		existingRecord.MarkAsDeleted(justification);
		await repository.UpdateAsync(existingRecord);
	}
}