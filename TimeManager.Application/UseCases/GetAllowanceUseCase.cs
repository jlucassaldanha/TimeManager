using TimeManager.Application.DTOs;
using TimeManager.Domain.Interfaces;

namespace TimeManager.Application.UseCases;

public class GetAllowanceUseCase(ITimeAllowanceRepository repository)
{
	public async Task<AllowanceDto> ExecuteAsync(DateOnly date)
	{

		var existingAllowance = await repository.GetByDateAllowanceAsync(date);

		if (existingAllowance == null)
            throw new InvalidOperationException("Abono não encontrado.");

		return new AllowanceDto(
			existingAllowance.Id,
			existingAllowance.Date,
			existingAllowance.HoursAllowed.ToString(@"hh\:mm"),
			existingAllowance.Justification
		);
	}
}