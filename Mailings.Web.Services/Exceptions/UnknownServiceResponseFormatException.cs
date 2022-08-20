namespace Mailings.Web.Services.Exceptions;
public sealed class UnknownServiceResponseFormatException : Exception
{
    public UnknownServiceResponseFormatException(string? message = null)   
        : this(message, null)
    {
    }
    public UnknownServiceResponseFormatException(
        string? message, 
        Exception? inner = null)
        : base(message, inner)
    {
    }
}