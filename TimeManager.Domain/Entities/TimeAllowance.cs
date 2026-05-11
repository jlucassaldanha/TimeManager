namespace TimeManager.Domain.Entities;

public class TimeAllowance
{
	public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateOnly Date { get; private set; }
	public TimeSpan HoursAllowed { get; private set; }
    public string Justification { get; private set; }
	public bool IsDeleted { get; private set; }
    public string? AuditJustification { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    protected TimeAllowance()
	{
		Justification = null!;
	}
	public TimeAllowance(Guid userId, DateOnly date, TimeSpan hoursAllowed, string justification)
	{
		if (userId == Guid.Empty) 
			throw new ArgumentException("User required");
        if (hoursAllowed.TotalMinutes <= 0) 
			throw new ArgumentException("Hours Allowed must be greater than zero");
        if (string.IsNullOrWhiteSpace(justification)) 
			throw new ArgumentException("Justification is required");

		Id = Guid.NewGuid();
        UserId = userId;
        Date = date; 
        HoursAllowed = hoursAllowed;
        Justification = justification;
		IsDeleted = false;
	}

	public void MarkAsDeleted(string auditJustification)
	{
		if (string.IsNullOrWhiteSpace(auditJustification))
			throw new ArgumentException("Audit Justification is required");

		IsDeleted = true;
        AuditJustification = auditJustification;
        UpdatedAt = DateTime.UtcNow;
	}

	public void Edit(TimeSpan newHoursAllowed, string newJustification, string auditJustification)
	{
		if (newHoursAllowed.TotalMinutes <= 0)
			throw new ArgumentException("Hours Allowed must be greater than zero");

		if (string.IsNullOrWhiteSpace(auditJustification))
			throw new ArgumentException("Audit Justification is required");

		if (string.IsNullOrWhiteSpace(newJustification))
			throw new ArgumentException("Justification is required");
		
		HoursAllowed = newHoursAllowed;
        Justification = newJustification;
        AuditJustification = auditJustification;
        UpdatedAt = DateTime.UtcNow;
	}
}