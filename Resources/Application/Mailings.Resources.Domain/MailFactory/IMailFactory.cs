using Mailings.Resources.Domain.Models;

namespace Mailings.Resources.Domain.MailFactory;

public interface IMailFactory
{
    public string ForUser { get; set; }

    Mail CreateTextMail(
        string theme,
        string text,
        IEnumerable<Attachment>? attachments = null);
    Mail CreateHtmlMail(
        string theme,
        string html,
        IEnumerable<Attachment>? attachments = null);
}