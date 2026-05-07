using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeManager.Domain.Entities;

namespace TimeManager.Infrastructure.Data.Mappings;

public class TimeRecordConfiguration : IEntityTypeConfiguration<TimeRecord>
{
	public void Configure(EntityTypeBuilder<TimeRecord> builder)
	{
		builder.ToTable("TimeRecords");

		builder.HasKey(t => t.Id);

		builder.Property(t => t.Type)
			.HasConversion<string>()
			.IsRequired()
			.HasMaxLength(20);

		builder.Property(t => t.Note)
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