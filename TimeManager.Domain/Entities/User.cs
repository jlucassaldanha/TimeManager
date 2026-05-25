namespace TimeManager.Domain.Entities;

public class User
{
	public Guid Id { get; private set; }
	public string Email { get; private set; }
	public string PasswordHash { get; private set; }
	public DateTime CreatedAt { get; private set; }

	protected User()
    {
        Email = null!;
        PasswordHash = null!;
    }
	
	public User(string email, string passwordHash)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Email não pode ser vazio.", nameof(email));
		
		Id = Guid.NewGuid();
		Email = email;
		PasswordHash = passwordHash;
		CreatedAt = DateTime.UtcNow;
	}

}