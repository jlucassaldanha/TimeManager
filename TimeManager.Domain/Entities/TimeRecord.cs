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

	protected TimeRecord() { }

	public TimeRecord(Guid userId, DateTime timestamp, RecordType type, string? note)
	{
		if (userId == Guid.Empty)
			throw new ArgumentException("User Id missing.", nameof(userId));

		Id = Guid.NewGuid();
		UserId = userId;
		Timestamp = timestamp;
		Type = type;
		Note = note;
	}
}