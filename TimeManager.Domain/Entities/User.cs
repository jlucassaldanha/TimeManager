namespace TimeManager.Domain.Entities;

public class User
{
	public Guid Id { get; private set; }
	public required string Name { get; set; }
	public required string Email { get; set; }
	public bool IsActive { get; private set; }

	protected User() { }

	public User(string name, string email)
	{
		if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
		if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");

		Id = Guid.NewGuid();
		Name = name;
		Email = email;
		IsActive = true;
	}

	public void Deactivate()
	{
		IsActive = false;
	}
}