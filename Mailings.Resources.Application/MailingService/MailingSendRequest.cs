using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Application.MailingService;
public sealed class MailingSendRequest
{
    //may later adding more info about request
    public MailingGroup Group { get; set; }
    public MailType MailType { get; set; }
}