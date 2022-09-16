using System.Runtime.Serialization;

namespace Mailings.Web.Exceptions;
[Serializable]
internal sealed class UrlIsNotGeneratedException : Exception
{
    public override string Message => base.Message ?? 
                                      "Url is not generated with current " +
                                      "action and controller by unknown reason.";

    public UrlIsNotGeneratedException(string? message = null)
        : this(message, null)
    {
    }
    public UrlIsNotGeneratedException(string? message, Exception? inner = null)
        : base(message, inner)
    {
    }

    private UrlIsNotGeneratedException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}