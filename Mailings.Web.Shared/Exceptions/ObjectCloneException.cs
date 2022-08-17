using System.Collections;
using Mailings.Web.Shared.Cloner;

namespace Mailings.Web.Shared.Exceptions;

[Serializable]
public class ObjectCloneException : Exception
{
    private readonly object _object;
    private readonly IDeepCloner _deepCloner;

    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["cloning object"] = _object,
        ["cloner"] = _deepCloner
    };

    public override string Message => base.Message ??
                                      "Error occurred when trying to clone object in " +
                                      $"{_deepCloner.GetType()} cloner. " +
                                      $"Cloning object: {_object}";

    public ObjectCloneException(
        object cloningObject,
        IDeepCloner deepCloner,
        string? message = null,
        Exception? inner = null)
        : base(message, inner)
    {
        _object = cloningObject;
        _deepCloner = deepCloner;
    }
}