namespace Mailings.Web.Shared.Dto;
public class MailingGroupDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid MailId { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string MailType { get; set; }
    public string SenderPseudo { get; set; }
    public IEnumerable<string> To { get; set; }
}