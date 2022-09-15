using System.Runtime.Serialization;

namespace Mailings.Authentication.Shared.Exceptions;
/// <summary>
///     Unknown identity client in system exception model
/// </summary>
public sealed class UnknownIdentityClientException : Exception
{
    /// <summary>
    ///     Message of exception model
    /// </summary>
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