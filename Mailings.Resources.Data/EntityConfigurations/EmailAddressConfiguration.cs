using Mailings.Resources.Domen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class EmailAddressConfiguration : IEntityTypeConfiguration<EmailAddress>
{
    public void Configure(EntityTypeBuilder<EmailAddress> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(em => em.Address).IsRequired();

        //builder.HasOne<EmailAddressTo>()
        //    .WithOne(t => t.Address)
        //    .HasForeignKey<EmailAddressTo>(t => t.Address)
        //    .OnDelete(DeleteBehavior.Cascade);
        //builder.HasOne<EmailAddressFrom>()
        //    .WithOne(t => t.Address)
        //    .HasForeignKey<EmailAddressFrom>(t => t.Address)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}