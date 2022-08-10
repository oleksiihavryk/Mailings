using Mailings.Resources.Data.EntityConfigurations;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.DbContexts;
public class CommonResourcesDbContext : DbContext
{
    public DbSet<MailingGroup> MailingGroups { get; set; } = null!;
    public DbSet<HtmlMail> HtmlMails { get; set; } = null!;
    public DbSet<TextMail> TextMails { get; set; } = null!;
    public DbSet<HistoryNoteMailingGroup> MailingHistory { get; set; } = null!;

    public CommonResourcesDbContext(
        DbContextOptions<CommonResourcesDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmailAddressConfiguration());
        builder.ApplyConfiguration(new EmailAddressToConfiguration());
        builder.ApplyConfiguration(new EmailAddressFromConfiguration());
        builder.ApplyConfiguration(new MailingGroupConfiguration());
        builder.ApplyConfiguration(new MailConfiguration());
        builder.ApplyConfiguration(new TextMailConfiguration());
        builder.ApplyConfiguration(new HtmlMailConfiguration());
        builder.ApplyConfiguration(new HistoryNoteConfiguration());
    }
}