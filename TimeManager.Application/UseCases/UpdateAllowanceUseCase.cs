using TimeManager.Domain.Entities;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class UpdateAllowanceUseCase(ITimeAllowanceRepository repository)
{
	public async Task ExecuteAsync(Guid id, TimeSpan newHoursAllowed, string newJustification, string auditJustification)
	{
		var existingAllowance = await repository.GetByIdAsync(id);

		if (existingAllowance == null)
            throw new InvalidOperationException("Registro de abono não encontrado.");

		existingAllowance.Edit(newHoursAllowed, newJustification, auditJustification);
		await repository.UpdateAsync(existingAllowance);
	}
}