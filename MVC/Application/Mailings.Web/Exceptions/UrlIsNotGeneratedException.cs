using System.Runtime.Serialization;

namespace Mailings.Web.Exceptions;
/// <summary>
///     Url is not generated exception model
/// </summary>
[Serializable]
internal sealed class UrlIsNotGeneratedException : Exception
{
    /// <summary>
    ///     Message of generated exception
    /// </summary>
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
}