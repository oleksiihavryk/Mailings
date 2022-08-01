namespace Mailings.Resources.Domen.Exceptions;

[Serializable]
public class MailingResponseException : Exception
{
    public MailingResponseException(Exception inner, string? message = null)
        : base(message, inner)
    {
    }
}