using System.Net;
using System.Net.Mail;
using Mailings.Resources.Domen.Exceptions;
using Mailings.Resources.Domen.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Mailings.Resources.Domen.MailingService;
public class MailingService : IMailingService
{
    private readonly MailSettings _mailSettings;

    public MailingService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public virtual MailingSendResponse Send(MailingSendRequest request)
        => SendAsync(request).GetAwaiter().GetResult();
    public virtual async Task<MailingSendResponse> SendAsync(MailingSendRequest request)
    {
        if (!request.Group.To.Any())
            throw new MailingRequestException(
                sendRequest: request,
                message: "Mailing group is not completed!" +
                " Values of emails that will receive message is missing!");

        if (!string.Equals(request.Group.UserId, request.Group.Mail.UserId,
                StringComparison.Ordinal))
            throw new MailingRequestException(
                sendRequest: request,
                message: "UserId is not equal in mail and in group!" +
                "Mail userId must to be equal to group user id");

        if (request.MailType == MailType.Unknown)
            throw new MailingRequestException(
                sendRequest: request,
                message: "Unknown mail type in mail request. " +
                         "Please, check assignment mail type to mail request.");

        if (!MailAddress.TryCreate(request.Group.From.Address.Address, out var parsedMail))
            throw new MailingRequestException(
                sendRequest: request,
                message: "Address from which email is sending is incorrect!" +
                "Please, check email format before sending mail");

        List<MailboxAddress> toMails = new List<MailboxAddress>();

        foreach (var toAddress in request.Group
                     .To
                     .Select(a => a.Address.Address))
        {
            if (!MailAddress.TryCreate(toAddress, out var toMail))
                throw new MailingRequestException(
                    sendRequest: request,
                    message: "Address to which email is sending is incorrect!" +
                    "Please, check email format before sending mail");
            toMails.Add(MailboxAddress.Parse(toMail.Address));
        }

        var mail = BuildMail(request, toMails, request.Group.From);

        try
        {
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                host: _mailSettings.Host,
                port: int.Parse(_mailSettings.Port),
                options: SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(new NetworkCredential(
                userName: _mailSettings.Mail,
                password: _mailSettings.Password));

            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(quit: true);
        }
        catch (Exception e)
        {
            throw new MailingResponseException(inner: e);
        }

        return new MailingSendResponse()
        {
            Group = request.Group,
            IsSucceded = true
        };
    }

    protected virtual MimeMessage BuildMail(
        MailingSendRequest request,
        IEnumerable<InternetAddress> toMails, 
        EmailAddressFrom fromMail)
    {
        var mail = new MimeMessage();

        mail.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        mail.To.AddRange(toMails);
        mail.Subject = $"From: {fromMail.Address.Address} ({fromMail.Name}). " +
                       $"Theme: {request.Group.Mail.Theme}";

        var builder = new BodyBuilder();

        if (request.Group.Mail.Attachments != null)
        {
            foreach (var file in request.Group.Mail.Attachments)
            {
                builder.Attachments.Add(
                    fileName: file.Name,
                    data: file.BytesData,
                    contentType: ContentType.Parse(file.ContentType));
            }
        }

        if (request.MailType.Equals(MailType.Html))
            builder.HtmlBody = request.Group.Mail.Content;
        else if (request.MailType.Equals(MailType.Text))
            builder.TextBody = request.Group.Mail.Content;

        mail.Body = builder.ToMessageBody();

        return mail;
    }
}