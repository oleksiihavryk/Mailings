using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;
public class MailingGroupConfiguration : IEntityTypeConfiguration<MailingGroup>
{
    public virtual void Configure(EntityTypeBuilder<MailingGroup> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.UserId).IsRequired();
        builder.Property(m => m.Name).IsRequired();

        builder.HasOne<Mail>(m => m.Mail)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}