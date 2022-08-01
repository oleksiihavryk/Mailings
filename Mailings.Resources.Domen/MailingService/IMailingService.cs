using Mailings.Resources.Domen.Models;

namespace Mailings.Resources.Domen.MailingService;

public interface IMailingService
{
    MailingSendResponse Send(MailingSendRequest request);
    Task<MailingSendResponse> SendAsync(MailingSendRequest request);
}