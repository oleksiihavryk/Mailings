using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Mailings.Resources.Domen;
using Mailings.Resources.Domen.Exceptions;
using Mailings.Resources.Domen.MailingService;
using Mailings.Resources.Domen.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using Xunit;
using Moq;

namespace Mailings.Resources.Tests;
public class MailingServiceTests
{
    private readonly MailSettings _testMailSettings =
        new MailSettings()
        {
            Host = "smtp.gmail.com",
            Mail = "mailings.sender@gmail.com",
            Password = "cgfvfmveysmxtedy",
            Port = "587"
        };

    [Theory]
    [InlineData(
        "ssss", 
        "textMessage",
        "I love antichrist",
        "aleksei.gavrik2004@gmail.com",
        "provka@gmail.com",
        "names is not matter",
        MailType.Text)]
    [InlineData(
        "ssss", 
        "htmlMessage", 
        "<h1>I love jesus</h1>", 
        "aleksei.gavrik2004gmail.com", 
        "provka@gmail.com", 
        "names is not matter",
        MailType.Html)]
    [InlineData(
        "ssss",
        "htmlMessage",
        "<h1>I love jesus</h1>",
        "aleksei.gavrik2004@gmail.com",
        "provkagmail.com",
        "names is not matter",
        MailType.Html)]
    [InlineData(
        "ssss",
        "unknownMessage", 
        "i love pariss", 
        "aleksei.gavrik2004@gmail.com",
        "aleksei.notgavrik2004@gmail.com", 
        "parash",
        MailType.Unknown)]
    public void MailingService_SendingMails(
        string userId, 
        string theme, 
        string message,
        string toAddress,
        string fromAddress, 
        string fromName,
        MailType type)
    {
        //arrange
        var mailingSettings = new Mock<IOptions<MailSettings>>();
        mailingSettings.Setup(p => p.Value)
            .Returns(_testMailSettings);

        var mailingService = new MailingService(mailingSettings.Object);

        var mailingGroup = new MailingGroup(string.Empty, userId);

        mailingGroup.Mail = type switch
        {
            MailType.Html => new HtmlMail(theme, userId) 
                { ByteContent = Encoding.UTF8.GetBytes(message) },
            MailType.Text => new TextMail(theme, userId)
                { StringContent = message },
            _ => new HtmlMail(theme, userId)
        };

        mailingGroup.From = new EmailAddressFrom()
        {
            Address = new EmailAddress() { Address = fromAddress },
            Name = fromName,
            Group = mailingGroup
        };
        mailingGroup.To = new List<EmailAddressTo>()
        {
            new EmailAddressTo()
            {
                Address = new EmailAddress() { Address = toAddress },
                Group = mailingGroup
            }
        };

        var request = new MailingSendRequest()
        {
            Group = mailingGroup,
            MailType = type
        };

        //act
        MailAddress? o = null;
        MailingSendResponse? response = null;

        if (!MailAddress.TryCreate(toAddress, out o) ||
            !MailAddress.TryCreate(fromAddress, out o) ||
            type == MailType.Unknown)
        {
            Assert.Throws<MailingRequestException>(() =>
            {
                response = mailingService.Send(request);
            });
        }
        else
        {
            response = mailingService.Send(request);
            Assert.True(response.IsSucceded);
            Assert.Equal(mailingGroup, response.Group);
        }
    }
}