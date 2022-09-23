namespace Mailings.Resources.Domain.Models;

public class EmailAddressFrom
{
    public Guid Id { get; set; }
    public MailingGroup Group { get; set; }
    public string? PseudoName { get; set; }
}