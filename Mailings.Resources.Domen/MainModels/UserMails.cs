namespace Mailings.Resources.Domain.MainModels;
public class UserMail
{
    public Guid Id { get; set; }
    public EmailAddress Address { get; set; }
    public string UserId { get; set; }
}