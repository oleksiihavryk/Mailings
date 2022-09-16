namespace Mailings.Web.Domain.Dto;
public sealed class MailingRequestDto
{
    public Guid MailingId { get; set; }
    public string SendType { get; set; } = string.Empty;
}