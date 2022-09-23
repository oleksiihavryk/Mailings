using System.ComponentModel.DataAnnotations.Schema;
using Mailings.Resources.Domain.MailFactory;

namespace Mailings.Resources.Domain.Models;

public abstract class Mail
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Theme { get; set; }
    [NotMapped]
    public abstract string Content { get; set; }
    public IEnumerable<Attachment>? Attachments { get; set; } = null;

    protected Mail(string theme, string userId)
    {
        Theme = theme;
        UserId = userId;
    }

    public static IMailFactory GetFactory(string userId) 
        => new MailFactory.MailFactory(userId);
}