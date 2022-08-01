using System;
using Mailings.Resources.Domen.Models;
using Mailings.Resources.Tests.TestClasses;
using Xunit;

namespace Mailings.Resources.Tests;
public class MailFactoryTests
{
    [Theory]
    [ClassData(typeof(CreatingMailData))]
    public void MailFactory_CreatingTextMail(
        string userId, 
        string theme,
        string text)
    {
        //Arrange
        var factory = new MailFactory(userId);
        var checkingAction = () =>
        {
            var textMail = factory.CreateTextMail(theme, text);

            Assert.IsType<TextMail>(textMail);
            Assert.Equal(theme, textMail.Theme);
            Assert.Equal(userId, textMail.UserId);
            Assert.Equal(text, textMail.Content);

            if (textMail is TextMail tm)
                Assert.Equal(text, tm.StringContent);
        };

        //Act and assert
        if (string.IsNullOrWhiteSpace(theme) ||
            string.IsNullOrWhiteSpace(text))
        {
            Assert.Throws<ArgumentException>(checkingAction);
        }
        else
        {
            checkingAction();
        }
    }
    [Theory]
    [ClassData(typeof(CreatingMailData))]
    public void MailFactory_CreatingHtmlMail(
        string userId,
        string theme,
        string htmlText)
    {
        //Arrange
        var factory = new MailFactory(userId);
        var checkingAction = () =>
        {
            var htmlMail = factory.CreateHtmlMail(theme, htmlText);

            Assert.IsType<HtmlMail>(htmlMail);
            Assert.Equal(theme, htmlMail.Theme);
            Assert.Equal(userId, htmlMail.UserId);
            Assert.Equal(htmlText, htmlMail.Content);

            if (htmlMail is HtmlMail tm)
                Assert.Equal(tm.Encoding.GetBytes(htmlText), tm.ByteContent);
        };

        //Act and asserting
        if (string.IsNullOrWhiteSpace(theme) ||
            string.IsNullOrWhiteSpace(htmlText))
        {
            Assert.Throws<ArgumentException>(checkingAction);
        }
        else
        {
            checkingAction();
        }
    }
}