namespace Mailings.Web.Domain.Dto;

public sealed class MailingResponseDto
{
    public bool IsSuccess { get; set; }
    public Guid MailingId { get; set; }
}