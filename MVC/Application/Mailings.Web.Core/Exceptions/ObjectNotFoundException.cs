namespace Mailings.Web.Core.Exceptions;
/// <summary>
///     Object not found exception model
/// </summary>
[Serializable]
public class ObjectNotFoundException : Exception
{
    public override string Message => base.Message ??
                                      "Object not found.";

    public ObjectNotFoundException(string? message = null)
        : base(message)
    {
    }
    public ObjectNotFoundException(string? message, Exception? inner = null)
        : base(message, inner)
    {
    }
}