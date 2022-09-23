using Mailings.Resources.Domain.Models;

namespace Mailings.Resources.Core.MailingService;
public sealed class MailingSendRequest
{
    //may later adding more info about request
    public MailingGroup Group { get; set; }
    public MailType MailType { get; set; }
}