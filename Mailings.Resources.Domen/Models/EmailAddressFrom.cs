namespace Mailings.Resources.Domen.Models;

public class EmailAddressFrom
{
    public Guid Id { get; set; }
    public MailingGroup Group { get; set; }
    public EmailAddress Address { get; set; }
    public string? Name { get; set; }
    public bool IsConfirmed { get; set; }
}