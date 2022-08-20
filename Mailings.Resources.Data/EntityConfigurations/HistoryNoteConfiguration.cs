using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class HistoryNoteConfiguration : IEntityTypeConfiguration<HistoryNoteMailingGroup>
{
    public virtual void Configure(EntityTypeBuilder<HistoryNoteMailingGroup> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.When).IsRequired();
        builder.Property(h => h.IsSucceded).IsRequired();
    }
}