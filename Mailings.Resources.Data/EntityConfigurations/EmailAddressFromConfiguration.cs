using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class EmailAddressFromConfiguration : IEntityTypeConfiguration<EmailAddressFrom>
{
    public void Configure(EntityTypeBuilder<EmailAddressFrom> builder)
    {
        builder.ToTable("EmailSenders");

        builder.HasKey(t => t.Id);

        builder.HasOne<MailingGroup>()
            .WithOne(te => te.From)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey<MailingGroup>();
    }
}