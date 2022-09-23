namespace Mailings.Resources.Core.Exceptions;

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