using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class MailConfiguration : IEntityTypeConfiguration<Mail>
{
    public virtual void Configure(EntityTypeBuilder<Mail> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.UserId).IsRequired();
        builder.Property(m => m.Theme).IsRequired();
    }
}