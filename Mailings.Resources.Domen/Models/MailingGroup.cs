using System.Collections;

namespace Mailings.Resources.Domen.Models;
public class MailingGroup 
{
    public Guid Id { get; set; } 
    public string UserId { get; set; }
    public string Name { get; set; }
    public IList<EmailAddressTo> To { get; set; }
        = Enumerable.Empty<EmailAddressTo>().ToList();
    public EmailAddressFrom From { get; set; }
    public Mail Mail { get; set; }

    public MailingGroup(string name, string userId)
        : this(name, userId, new EmailAddress())
    {
    }
    public MailingGroup(string name, string userId, EmailAddress from)
    {
        UserId = userId;
        Name = name;
        From = new EmailAddressFrom()
        {
            Address = from,
            Group = this
        };
    }
}