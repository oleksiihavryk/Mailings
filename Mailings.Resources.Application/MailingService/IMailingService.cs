namespace Mailings.Resources.Application.MailingService;

public interface IMailingService
{
    MailingSendResponse Send(MailingSendRequest request);
    Task<MailingSendResponse> SendAsync(MailingSendRequest request);
}