namespace Mailings.Web.Shared.Dto;
public class MailDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string UserId { get; set; }
    public string Content { get; set; }
    public string Theme { get; set; }
    public IEnumerable<AttachmentDto> Attachments { get; set; } =
        new List<AttachmentDto>();
}