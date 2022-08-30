using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.API.Dto;

public sealed class MailDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public IEnumerable<AttachmentDto> Attachments { get; set; } =
        new List<AttachmentDto>();

    public MailDto()
    {
    }
    public MailDto(Mail mail)
    {
        Content = mail.Content;
        Id = mail.Id;
        UserId = mail.UserId;
        Theme = mail.Theme;

        var attachments = new List<AttachmentDto>();
        foreach (var a in mail.Attachments ?? Enumerable.Empty<Attachment>())
        {
            attachments.Add(new AttachmentDto()
            {
                BytesContent = a.BytesData,
                ContentType = a.ContentType,
                Id = a.Id,
                Name = a.Name
            });
        }

        Attachments = attachments;
    }

    public static explicit operator HtmlMail(MailDto dto)
    {
        var mailFactory = Mail.GetFactory(dto.UserId);
        HtmlMail mail = (HtmlMail)mailFactory.CreateHtmlMail(
            theme: dto.Theme,
            html: dto.Content);

        mail.Id = dto.Id;

        var attachments = new List<Attachment>();
        foreach (var a in dto.Attachments)
            attachments.Add(new Attachment()
            {
                ContentType = a.ContentType,
                BytesData = a.BytesContent,
                Name = a.Name,
                Mail = mail,
                Id = a.Id
            });

        mail.Attachments = attachments;

        return mail;
    }
    public static explicit operator TextMail(MailDto dto)
    {
        var mailFactory = Mail.GetFactory(dto.UserId);
        TextMail mail = (TextMail)mailFactory.CreateTextMail(
            theme: dto.Theme,
            text: dto.Content);

        mail.Id = dto.Id;

        var attachments = new List<Attachment>();
        foreach (var a in dto.Attachments)
            attachments.Add(new Attachment()
            {
                ContentType = a.ContentType,
                BytesData = a.BytesContent,
                Name = a.Name,
                Mail = mail,
                Id = a.Id
            });

        mail.Attachments = attachments;

        return mail;
    }
    public static explicit operator MailDto(Mail mail)
    {
        return new MailDto(mail);
    }
}