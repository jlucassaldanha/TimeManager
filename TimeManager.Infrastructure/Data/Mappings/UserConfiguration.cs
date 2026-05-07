using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeManager.Domain.Entities;

namespace TimeManager.Infrastructure.Data.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasKey(u => u.Id);

		builder.Property(u => u.Name)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(100);

		builder.HasIndex(u => u.Email)
			.IsUnique();
	}
}