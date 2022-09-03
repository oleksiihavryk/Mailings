using System.Runtime.Serialization;

namespace Mailings.Authentication.Shared.Exceptions;
public sealed class UnknownIdentityClientException : Exception
{
    public override string Message => base.Message ??
                                      "Unknown identity client. Please choose " +
                                      "the right one or add some more clients in system.";

    public UnknownIdentityClientException(string? message = null)
        : this(message, null)
    {
    }
    public UnknownIdentityClientException(
        string? message, 
        Exception? inner = null)
        : base(message, inner)
    {
    }

    private UnknownIdentityClientException(
        StreamingContext context, 
        SerializationInfo info)
        : base(info, context)
    {
    }
}