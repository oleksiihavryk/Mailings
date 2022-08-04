using System.Net.Mail;
using Mailings.Resources.Application.Exceptions;
using Mailings.Resources.Domain;
using Mailings.Resources.Domain.MainModels;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Mailings.Resources.Application.MailingService;
public class MailingService : IMailingService
{
    private readonly MailSettings _mailSettings;

    public MailingService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public MailingSendResponse Send(MailingSendRequest request)
    {
        CheckRequest(request);

        var mail = CreateMail(request);

        using var smtpClient = new SmtpClient();

        MailingSendResponse? response = null;

        try
        {
            smtpClient.Connect(
                host: _mailSettings.Host,
                port: int.Parse(_mailSettings.Port));
            smtpClient.Authenticate(
                userName: _mailSettings.Mail,
                password: _mailSettings.Password);

            smtpClient.Send(mail);
            smtpClient.Disconnect(true);

            response = new MailingSendResponse()
            {
                IsSucceded = true,
                Group = request.Group
            };
        }
        catch (SslHandshakeException)
        {
            response = new MailingSendResponse()
            {
                IsSucceded = false,
                Group = request.Group
            };
        }

        return response;
    }
    public async Task<MailingSendResponse> SendAsync(MailingSendRequest request)
    {
        CheckRequest(request);

        var mail = CreateMail(request);

        using var smtpClient = new SmtpClient();

        MailingSendResponse? response = null;

        try
        {
            await smtpClient.ConnectAsync(
                host: _mailSettings.Host,
                port: int.Parse(_mailSettings.Port));
            await smtpClient.AuthenticateAsync(
                userName: _mailSettings.Mail,
                password: _mailSettings.Password);

            await smtpClient.SendAsync(mail);
            
            await smtpClient.DisconnectAsync(true);

            response = new MailingSendResponse()
            {
                IsSucceded = true,
                Group = request.Group
            };
        }
        catch (SslHandshakeException)
        {
            response = new MailingSendResponse()
            {
                IsSucceded = false,
                Group = request.Group
            };
        }

        return response;
    }

    protected MimeMessage CreateMail(MailingSendRequest request)
    {
        var mail = new MimeMessage();

        mail.Sender = MailboxAddress.Parse(_mailSettings.Mail);

        var toAddresses = CreateEnumerableOfReceiverAddresses(
            from: request);
        mail.To.AddRange(toAddresses);

        var group = request.Group;
        mail.Subject = $"Mailings From: {group.From.Address.AddressString} " +
                       $"({group.From.Name}). " +
                       $"Theme: {group.Mail.Theme}";

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
    protected void CheckRequest(MailingSendRequest request)
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

        if (!MailAddress.TryCreate(request.Group.From.Address.AddressString, out var parsedMail))
            throw new MailingRequestException(
                sendRequest: request,
                message: "Address from which email is sending is incorrect!" +
                "Please, check email format before sending mail");

        List<MailboxAddress> toMails = new List<MailboxAddress>();

        foreach (var toAddress in request.Group
                     .To
                     .Select(a => a.Address.AddressString))
        {
            if (!MailAddress.TryCreate(toAddress, out var toMail))
                throw new MailingRequestException(
                    sendRequest: request,
                    message: "Address to which email is sending is incorrect!" +
                    "Please, check email format before sending mail");
            toMails.Add(MailboxAddress.Parse(toMail.Address));
        }
    }

    private IEnumerable<InternetAddress> CreateEnumerableOfReceiverAddresses(
        MailingSendRequest from)
    {
        List<MailboxAddress> addresses = new List<MailboxAddress>();
        foreach (var to in from.Group.To)
            addresses.Add(MailboxAddress.Parse(to.Address.AddressString));
        return addresses;
    }
}