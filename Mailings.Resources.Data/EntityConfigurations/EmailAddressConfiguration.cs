using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class EmailAddressConfiguration : IEntityTypeConfiguration<EmailAddress>
{
    public virtual void Configure(EntityTypeBuilder<EmailAddress> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(em => em.AddressString).IsRequired();
    }
}