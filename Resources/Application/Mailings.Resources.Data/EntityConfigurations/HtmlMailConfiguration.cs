using Mailings.Resources.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;

public class HtmlMailConfiguration : IEntityTypeConfiguration<HtmlMail>
{
    public virtual void Configure(EntityTypeBuilder<HtmlMail> builder)
    {
        builder.Property(hm => hm.ByteContent).IsRequired(false);
    }
}