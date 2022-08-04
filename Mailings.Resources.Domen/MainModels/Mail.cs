using Mailings.Resources.Domain.MailFactory;

namespace Mailings.Resources.Domain.MainModels;

public abstract class Mail
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Theme { get; set; }
    public abstract string Content { get; }
    public IEnumerable<Attachment>? Attachments { get; set; } = null;

    protected Mail(string theme, string userId)
    {
        Theme = theme;
        UserId = userId;
    }

    public static IMailFactory GetFactory(string userId) => new MailFactory.MailFactory(userId);
}