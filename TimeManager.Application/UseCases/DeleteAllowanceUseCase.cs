using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class DeleteAllowanceUseCase(ITimeAllowanceRepository repository)
{
	public async Task ExecuteAsync(Guid id, string justification)
	{
		var existingAllowance = await repository.GetByIdAsync(id);

		if (existingAllowance == null)
            throw new InvalidOperationException("Registro de abono não encontrado.");
        
		existingAllowance.MarkAsDeleted(justification);
		await repository.UpdateAsync(existingAllowance);
	}
}
