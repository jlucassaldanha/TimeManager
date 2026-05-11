using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeManager.Domain.Entities;

namespace TimeManager.Infrastructure.Data.Mappings;

public class TimeAllowanceConfiguration : IEntityTypeConfiguration<TimeAllowance>
{
	public void Configure(EntityTypeBuilder<TimeAllowance> builder)
	{
		builder.ToTable("TimeAllowance");

		builder.HasKey(t => t.Id);

		builder.Property(t => t.Justification)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(t => t.AuditJustification)
			.HasMaxLength(500);

		builder.HasQueryFilter(t => !t.IsDeleted);

		builder.HasOne<User>()
			.WithMany()
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}