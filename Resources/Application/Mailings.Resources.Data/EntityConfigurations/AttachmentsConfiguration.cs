using Mailings.Resources.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mailings.Resources.Data.EntityConfigurations;
public class AttachmentsConfiguration : IEntityTypeConfiguration<Attachment>
{
    public virtual void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Mail)
            .WithMany(m => m.Attachments)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}