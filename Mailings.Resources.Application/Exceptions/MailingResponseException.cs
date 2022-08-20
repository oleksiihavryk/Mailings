namespace Mailings.Resources.Application.Exceptions;

[Serializable]
internal class MailingResponseException : Exception
{
    public MailingResponseException(
        Exception inner, 
        string? message = null)
        : base(message, inner)
    {
    }
}