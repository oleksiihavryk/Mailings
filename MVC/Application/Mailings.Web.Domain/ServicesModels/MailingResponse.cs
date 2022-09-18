namespace Mailings.Web.Domain.ServicesModels;

public sealed class MailingResponse
{
    public bool IsSuccess { get; set; }
    public Guid MailingId { get; set; }
}