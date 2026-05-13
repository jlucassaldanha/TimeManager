using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeManager.Domain.Entities;

namespace TimeManager.Infrastructure.Data.Mappings;

public class WorkJourneyRuleConfiguration : IEntityTypeConfiguration<WorkJourneyRule>
{
	public void Configure(EntityTypeBuilder<WorkJourneyRule> builder)
	{
		builder.ToTable("WorkJourneyRules");

		builder.HasKey(w => w.Id);
	}
}