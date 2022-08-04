using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Domain.MailFactory;

public interface IMailFactory
{
    public string ForUser { get; set; }

    Mail CreateTextMail(string theme, string text);
    Mail CreateHtmlMail(string theme, string html);
}