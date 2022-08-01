using Mailings.Resources.Domen.MailingService;
using Mailings.Resources.Domen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class HistoryNoteConfiguration : IEntityTypeConfiguration<HistoryNoteMailingGroup>
{
    public void Configure(EntityTypeBuilder<HistoryNoteMailingGroup> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.When).IsRequired();
        builder.Property(h => h.IsSucceded).IsRequired();

        //builder.HasOne(h => h.Group)
        //    .WithOne()
        //    .OnDelete(DeleteBehavior.NoAction)
        //    .HasForeignKey<MailingGroup>();
    }
}