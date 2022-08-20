namespace Mailings.Web.Shared.Dto;
public sealed class MailDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public IEnumerable<AttachmentDto> Attachments { get; set; } =
        new List<AttachmentDto>();
}