using System.Net.Mime;
using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Domain.MailFactory;

public class MailFactory : IMailFactory
{
    public string ForUser { get; set; }

    public MailFactory()
    {
        ForUser = string.Empty;
    }
    public MailFactory(string forUser)
    {
        ForUser = forUser;
    }

    public virtual Mail CreateTextMail(
        string theme, 
        string text,
        IEnumerable<Attachment>? attachments = null)
    {
        if (string.IsNullOrWhiteSpace(ForUser))
        {
            throw new ArgumentException(
                message: $"Argument {nameof(ForUser)} cannot be null or empty.",
                paramName: nameof(ForUser));
        }
        if (string.IsNullOrWhiteSpace(theme))
        {
            throw new ArgumentException(
                message: $"Argument {nameof(theme)} cannot be null or empty.",
                paramName: nameof(theme));
        }
        if (attachments != null && !CheckAttachmentsData(attachments))
        {
            throw new ArgumentException(
                message: $"Argument {nameof(attachments)} is include invalid data",
                paramName: nameof(attachments));
        }

        var mail = new TextMail(theme, ForUser)
        {
            Attachments = attachments
        };
        mail.StringContent = text;
        return mail;
    }
    public virtual Mail CreateHtmlMail(
        string theme, 
        string html, 
        IEnumerable<Attachment>? attachments = null)
    {
        if (string.IsNullOrWhiteSpace(ForUser))
        {
            throw new ArgumentException(
                message: $"Argument {nameof(ForUser)} cannot be null or empty.",
                paramName: nameof(ForUser));
        }
        if (string.IsNullOrWhiteSpace(theme))
        {
            throw new ArgumentException(
                message: $"Argument {nameof(theme)} cannot be null or empty.",
                paramName: nameof(theme));
        }
        if (attachments != null && !CheckAttachmentsData(attachments))
        {
            throw new ArgumentException(
                message: $"Argument {nameof(attachments)} is include invalid data",
                paramName: nameof(attachments));
        }

        var mail = new HtmlMail(theme, ForUser)
        {
            Attachments = attachments
        };
        mail.Content = html;
        return mail;
    }

    private bool CheckAttachmentsData(IEnumerable<Attachment> attachments)
    {
        foreach (var a in attachments)
            if (string.IsNullOrWhiteSpace(a.Data) ||
                string.IsNullOrWhiteSpace(a.ContentType))
                return false;
        return true;
    }
}