using Mailings.Resources.Domen.Models;

namespace Mailings.Resources.Domen.MailingService;
public class MailingSendRequest
{
    //may later adding more info about request
    public MailingGroup Group { get; set; }
    public MailType MailType { get; set; }
}