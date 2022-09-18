namespace Mailings.Web.Domain.ServicesModels;
public sealed class MailingRequest
{
    public Guid MailingId { get; set; }
    public string SendType { get; set; } = string.Empty;
}