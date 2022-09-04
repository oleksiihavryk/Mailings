using System.Runtime.Serialization;

namespace Mailings.Web.Services.Exceptions;
[Serializable]
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

    public UnknownServiceResponseFormatException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}