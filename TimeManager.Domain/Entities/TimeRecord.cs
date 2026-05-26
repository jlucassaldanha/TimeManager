namespace TimeManager.Domain.Entities;

public enum RecordType
{
	Entry,
	Exit
}

public class TimeRecord
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }
	public DateTime Timestamp { get; private set; }
	public RecordType Type { get; private set; }
	public string? Note { get; private set; }
	public bool IsDeleted { get; private set; }
    public string? AuditJustification { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

	protected TimeRecord() { }

	public TimeRecord(Guid userId, DateTime timestamp, RecordType type, string? note)
	{
		if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required.");
			
		Id = Guid.NewGuid();
		UserId = userId;
		Timestamp = timestamp;
		Type = type;
		Note = note;
		IsDeleted = false;
	}

	public void MarkAsDeleted(string auditJustification)
	{
		if (string.IsNullOrWhiteSpace(auditJustification))
			throw new ArgumentException("Justification is required");

		IsDeleted = true;
		AuditJustification = auditJustification;
		UpdatedAt = DateTime.UtcNow;
	}

	public void UpdateDetails(DateTime newTimestamp, RecordType newType, string auditJustification)
	{
		if (IsDeleted)
			throw new InvalidOperationException("Não é possível alterar um registro apagado.");
			
		if (string.IsNullOrWhiteSpace(auditJustification))
			throw new ArgumentException("Justification is required");

		Timestamp = newTimestamp;
		Type = newType;
		AuditJustification = auditJustification;
		Note = auditJustification;
		UpdatedAt = DateTime.UtcNow;
	}
}