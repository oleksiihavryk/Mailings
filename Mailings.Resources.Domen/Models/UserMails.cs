namespace Mailings.Resources.Domen.Models;
public class UserMails
{
    public Guid Id { get; set; }
    public EmailAddress Address { get; set; }
    public string UserId { get; set; }
}