using TimeManager.Domain.Entities;

namespace TimeManager.Domain.Services;

public class AllowanceService
{
	public void ValidateAllowanceRequest(TimeSpan requestDuration, TimeSpan dailyGoal)
	{
		if (dailyGoal == TimeSpan.Zero)
			throw new InvalidOperationException("O usuario não possui carga horaria para abonar.");

		if (requestDuration > dailyGoal)
			throw new InvalidOperationException("Excede o limite de abono.");
	}

	public TimeSpan CalculateEffectiveAllowance(TimeSpan totalWorkedHours, TimeSpan totalAllowanceClaimed, TimeSpan dailyGoal)
	{
		if (totalWorkedHours >= dailyGoal) return TimeSpan.Zero;

		var missingHours = dailyGoal - totalWorkedHours;
		return totalAllowanceClaimed <= missingHours ? totalAllowanceClaimed : missingHours;
	}
}