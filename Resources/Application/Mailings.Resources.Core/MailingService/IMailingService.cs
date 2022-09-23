namespace Mailings.Resources.Core.MailingService;

public interface IMailingService
{
    MailingSendResponse Send(MailingSendRequest request);
    Task<MailingSendResponse> SendAsync(MailingSendRequest request);
}