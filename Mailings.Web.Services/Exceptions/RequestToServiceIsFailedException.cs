using System.Collections;
using System.Runtime.Serialization;
using Mailings.Web.Shared.Extensions;

namespace Mailings.Web.Services.Exceptions;
[Serializable]
public sealed class RequestToServiceIsFailedException : Exception
{
    private readonly string _nameOfService;

    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["service name"] = _nameOfService
    };

    public override string Message => base.Message ??
                                      $"Request to service by name {_nameOfService} " +
                                      $"is failed.";

    public RequestToServiceIsFailedException(
        string nameOfService, 
        string? message = null)
        : this(nameOfService, message, null)
    {
    }
    public RequestToServiceIsFailedException(
        string nameOfService,
        string? message,
        Exception? inner = null)
        : base(message, inner)
    {
        _nameOfService = nameOfService;
    }

    private RequestToServiceIsFailedException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
        _nameOfService = info.GetObject<string>(nameof(_nameOfService));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(_nameOfService), _nameOfService);
    }
}