namespace Mailings.Resources.Domen.Models;

public interface IMailFactory
{
    public string ForUser { get; set; }

    Mail CreateTextMail(string theme, string text);
    Mail CreateHtmlMail(string theme, string html);
}