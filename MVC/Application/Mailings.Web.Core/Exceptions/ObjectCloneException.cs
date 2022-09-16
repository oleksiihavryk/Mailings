using System.Collections;
using System.Runtime.Serialization;
using Mailings.Web.Core.Cloner;
using Mailings.Web.Shared.Extensions;

namespace Mailings.Web.Core.Exceptions;
[Serializable]
public sealed class ObjectCloneException : Exception
{
    private readonly object? _object;
    private readonly IDeepCloner _deepCloner;

    public override IDictionary Data => new Dictionary<string, object?>()
    {
        ["cloning object"] = _object,
        ["cloner"] = _deepCloner
    };

    public override string Message => base.Message ??
                                      "Error occurred when trying to clone object in " +
                                      $"{_deepCloner.GetType()} cloner. " +
                                      $"Cloning object: {_object}";

    public ObjectCloneException(
        IDeepCloner deepCloner,
        object? cloningObject,
        string? message = null)
        : this(deepCloner, cloningObject, message, null)
    {
    }
    public ObjectCloneException(
        IDeepCloner deepCloner,
        object? cloningObject,
        string? message,
        Exception? inner = null)
        : base(message, inner)
    {
        _object = cloningObject;
        _deepCloner = deepCloner;
    }

    private ObjectCloneException(
        SerializationInfo info,
        StreamingContext context)
        :base(info, context)
    {
        _object = info.GetObject<object>(nameof(_object));
        _deepCloner = info.GetObject<IDeepCloner>(nameof(_deepCloner));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(_object), _object);
        info.AddValue(nameof(_deepCloner), _deepCloner);
    }
}