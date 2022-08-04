namespace Mailings.Resources.Domain.MainModels;

public class TextMail : Mail
{
    public string StringContent { get; set; } = string.Empty;
    public override string Content => StringContent;

    public TextMail(string theme, string userId) 
        : base(theme, userId)
    {
    }
}