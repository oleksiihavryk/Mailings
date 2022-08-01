namespace Mailings.Resources.Shared.Dto;

public abstract class MailDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Theme { get; set; }
    public abstract string Content { get; }

    protected MailDto(string theme, string userId)
    {
        Theme = theme;
        UserId = userId;
    }
}