using System.Text;
using Mailings.Resources.Domain.MailFactory;
using Mailings.Resources.Domain.Models;
using Mailings.Resources.Tests.Comparer;

namespace Mailings.Resources.Tests;
public class MailFactoryTests
{
    private IMailComparer _comparer = new MailComparer();

    [Fact]
    public void MailFactory_CreatingHtmlMailWithEmptyUserId_ShouldThrowException()
    {
        //arrange
        string theme = "1";
        string html = "1";
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = Convert.ToBase64String(Encoding.UTF8.GetBytes("1")),
                ContentType = "text/plain",
                Name = "1"
            }
        };
        //act
        void ActAction()
        {
            IMailFactory mailFactory = new MailFactory();//empty user id realization
            var mail = mailFactory.CreateHtmlMail(theme, html, attachments);
        }
        //assert
        Assert.Throws<ArgumentException>(ActAction);
    }
    [Fact]
    public void MailFactory_CreatingHtmlMailWithEmptyTheme_ShouldThrowException()
    {
        //arrange
        string theme = string.Empty;
        string html = "1";
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = Convert.ToBase64String(Encoding.UTF8.GetBytes("1")),
                ContentType = "text/plain",
                Name = "1"
            }
        };
        string userId = "1";
        //act
        void ActAction()
        {
            IMailFactory mailFactory = new MailFactory(forUser: userId);
            var mail = mailFactory.CreateHtmlMail(theme, html, attachments);
        }
        //assert
        Assert.Throws<ArgumentException>(ActAction);
    }
    [Fact]
    public void MailFactory_CreatingHtmlMailWithEmptyContent_ShouldReturnTrue()
    {
        //arrange
        string theme = "1";
        string html = string.Empty;
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = Convert.ToBase64String(Encoding.UTF8.GetBytes("1")),
                ContentType = "text/plain",
                Name = "1"
            }
        };
        string userId = "1";
        //act
        IMailFactory mailFactory = new MailFactory(forUser: userId);
        var mail = mailFactory.CreateHtmlMail(theme, html, attachments);
        //assert
        var expectedMail = new HtmlMail(theme, userId) {Attachments = attachments};
        expectedMail.ByteContent = expectedMail.Encoding.GetBytes(html); 
        Assert.Equal(
            expected: expectedMail,
            actual: mail,
            comparer: _comparer);
    }
    [Fact]
    public void MailFactory_CreatingHtmlMailWithEmptyAttachments_ShouldReturnTrue()
    {
        //arrange
        string theme = "1";
        string html = "1";
        string userId = "1";
        //act
        IMailFactory mailFactory = new MailFactory(forUser: userId);
        var mail = mailFactory.CreateHtmlMail(theme, html);
        //assert
        var expectedMail = new HtmlMail(theme, userId);
        expectedMail.ByteContent = expectedMail.Encoding.GetBytes(html);
        Assert.Equal(
            expected: expectedMail,
            actual: mail,
            comparer: _comparer);
    }
    [Fact]
    public void MailFactory_CreatingHtmlMailWithInvalidAttachments_ShouldThrowException()
    {
        //arrange
        string theme = "1";
        string html = "1";
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = string.Empty,
                ContentType = string.Empty,
                Name = string.Empty
            }
        };
        string userId = "1";
        //act
        void ActAction()
        {
            IMailFactory mailFactory = new MailFactory(forUser: userId);
            var mail = mailFactory.CreateHtmlMail(theme, html, attachments);
        }
        //assert
        Assert.Throws<ArgumentException>(ActAction);
    }
    [Fact]
    public void MailFactory_CreatingTextMailWithEmptyUserId_ShouldThrowException()
    {
        //arrange
        string theme = "1";
        string text = "1";
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = Convert.ToBase64String(Encoding.UTF8.GetBytes("1")),
                ContentType = "text/plain",
                Name = "1"
            }
        };
        //act
        void ActAction()
        {
            IMailFactory mailFactory = new MailFactory();//empty user id realization
            var mail = mailFactory.CreateTextMail(theme, text, attachments);
        }
        //assert
        Assert.Throws<ArgumentException>(ActAction);
    }
    [Fact]
    public void MailFactory_CreatingTextMailWithEmptyTheme_ShouldThrowException()
    {
        //arrange
        string theme = string.Empty;
        string text = "1";
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = Convert.ToBase64String(Encoding.UTF8.GetBytes("1")),
                ContentType = "text/plain",
                Name = "1"
            }
        };
        string userId = "1";
        //act
        void ActAction()
        {
            IMailFactory mailFactory = new MailFactory(forUser: userId);
            var mail = mailFactory.CreateTextMail(theme, text, attachments);
        }
        //assert
        Assert.Throws<ArgumentException>(ActAction);
    }
    [Fact]
    public void MailFactory_CreatingTextMailWithEmptyContent_ShouldReturnTrue()
    {
        //arrange
        string theme = "1";
        string text = string.Empty;
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = Convert.ToBase64String(Encoding.UTF8.GetBytes("1")),
                ContentType = "text/plain",
                Name = "1"
            }
        };
        string userId = "1";
        //act
        IMailFactory mailFactory = new MailFactory(forUser: userId);
        var mail = mailFactory.CreateTextMail(theme, text, attachments);
        //assert
        var expectedMail = new HtmlMail(theme, userId) { Attachments = attachments };
        expectedMail.ByteContent = expectedMail.Encoding.GetBytes(text);
        Assert.Equal(
            expected: expectedMail,
            actual: mail,
            comparer: _comparer);
    }
    [Fact]
    public void MailFactory_CreatingTextMailWithEmptyAttachments_ShouldReturnTrue()
    {
        //arrange
        string theme = "1";
        string text = "1";
        List<Attachment> attachments = new List<Attachment>
        {
        };
        string userId = "1";
        //act
        IMailFactory mailFactory = new MailFactory(forUser: userId);
        var mail = mailFactory.CreateTextMail(theme, text, attachments);
        //assert
        var expectedMail = new TextMail(theme, userId) { Attachments = attachments };
        expectedMail.StringContent = text;
        Assert.Equal(
            expected: expectedMail,
            actual: mail,
            comparer: _comparer);
    }
    [Fact]
    public void MailFactory_CreatingTextMailWithInvalidAttachments_ShouldThrowException()
    {
        //arrange
        string theme = "1";
        string text = "1";
        List<Attachment> attachments = new List<Attachment>()
        {
            new Attachment()
            {
                Data = string.Empty,
                ContentType = string.Empty,
                Name = string.Empty
            }
        };
        string userId = "1";
        //act
        void ActAction()
        {
            IMailFactory mailFactory = new MailFactory(forUser: userId);
            var mail = mailFactory.CreateTextMail(theme, text, attachments);
        }
        //assert
        Assert.Throws<ArgumentException>(ActAction);
    }
}