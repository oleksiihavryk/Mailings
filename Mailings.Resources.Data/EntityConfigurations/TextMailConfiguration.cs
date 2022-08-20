using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class TextMailConfiguration : IEntityTypeConfiguration<TextMail>
{
    public virtual void Configure(EntityTypeBuilder<TextMail> builder)
    {
        builder.Property(t => t.StringContent).IsRequired(false);
    }
}