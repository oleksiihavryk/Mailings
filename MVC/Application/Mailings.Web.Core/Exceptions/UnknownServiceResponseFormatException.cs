using System.Runtime.Serialization;

namespace Mailings.Web.Core.Exceptions;
/// <summary>
///     Unknown service response format exception model
/// </summary>
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