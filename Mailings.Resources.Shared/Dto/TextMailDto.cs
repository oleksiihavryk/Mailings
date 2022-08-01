namespace Mailings.Resources.Shared.Dto;

public class TextMailDto : MailDto
{
    public string StringContent { get; set; } = string.Empty;
    public override string Content => StringContent;

    public TextMailDto(string theme, string userId)
        : base(theme, userId)
    {
    }
}