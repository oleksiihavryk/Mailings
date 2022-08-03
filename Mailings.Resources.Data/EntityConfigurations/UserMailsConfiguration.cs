using Mailings.Resources.Domen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class UserMailsConfiguration : IEntityTypeConfiguration<UserMails>
{
    public void Configure(EntityTypeBuilder<UserMails> builder)
    {
        builder.HasKey(um => um.Id);

        builder.HasOne<EmailAddress>()
            .WithOne()
            .HasForeignKey<UserMails>()
            .OnDelete(DeleteBehavior.NoAction);
    }
}