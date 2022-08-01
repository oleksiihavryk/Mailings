using System.Text;

namespace Mailings.Resources.Domen.Models;

public class MailFactory : IMailFactory
{
    public string ForUser { get; set; }

    public MailFactory(string forUser)
    {
        ForUser = forUser;
    }

    public Mail CreateTextMail(string theme, string text)
    {
        if (string.IsNullOrWhiteSpace(theme))
            throw new ArgumentException(
                $"Argument {nameof(theme)} is null or whitespace");

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException(
                $"Argument {nameof(theme)} is null or whitespace");

        return new TextMail(theme, ForUser)
        {
            StringContent = text
        };
    }
    public Mail CreateHtmlMail(string theme, string html)
    {
        if (string.IsNullOrWhiteSpace(theme))
            throw new ArgumentException(
                $"Argument {nameof(theme)} is null or whitespace");

        if (string.IsNullOrWhiteSpace(html))
            throw new ArgumentException(
                $"Argument {nameof(theme)} is null or whitespace");

        var htmlMail = new HtmlMail(theme, ForUser);
        
        htmlMail.ByteContent = htmlMail.Encoding.GetBytes(html);
        
        return htmlMail;
    }
}