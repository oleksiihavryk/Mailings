namespace Mailings.Resources.Dto;

public sealed class MailingGroupDto
{
    public Guid? Id { get; set; } = null;
    public Guid MailId { get; set; } 
    public string UserId { get; set; } 
    public string Name { get; set; } 
    public string MailType { get; set; } 
    public string SenderPseudo { get; set; } 
    public IEnumerable<string> To { get; set; } 
}