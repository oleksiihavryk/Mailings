namespace Mailings.Web.Domain.ServicesModels;
public sealed class MailingGroup
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid MailId { get; set; } = Guid.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string MailType { get; set; } = string.Empty;
    public string SenderPseudo { get; set; } = string.Empty;
    public IEnumerable<string> To { get; set; } = Array.Empty<string>();
}