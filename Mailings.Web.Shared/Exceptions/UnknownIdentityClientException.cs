using System.Runtime.Serialization;

namespace Mailings.Web.Shared.Exceptions;
[Serializable]
public sealed class UnknownIdentityClientException : Exception
{
    public override string Message => base.Message ??
                                      "Unknown identity client. Please check your clients " +
                                      "before start";

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
}