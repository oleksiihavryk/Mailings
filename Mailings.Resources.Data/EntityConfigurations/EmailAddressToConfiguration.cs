using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class EmailAddressToConfiguration : IEntityTypeConfiguration<EmailAddressTo>
{
    public void Configure(EntityTypeBuilder<EmailAddressTo> builder)
    {
        builder.ToTable("EmailReceivers");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Group)
            .WithMany(m => m.To)
            .OnDelete(DeleteBehavior.NoAction);
    }
}