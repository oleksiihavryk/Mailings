using System.Collections;
using System.Runtime.Serialization;
using Mailings.Web.Shared.Extensions;

namespace Mailings.Web.Services.Exceptions;
[Serializable]
public sealed class UnknownResponseBodyFromRequestToServiceException : Exception
{
    private readonly string _nameOfService;

    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["service name"] = _nameOfService
    };

    public override string Message => base.Message ??
                                      $"Unknown body response from service {_nameOfService}";

    public UnknownResponseBodyFromRequestToServiceException(
        string nameOfService,
        string? message = null)
        : this(nameOfService, message, null)
    {
    }
    public UnknownResponseBodyFromRequestToServiceException(
        string nameOfService,
        string? message,
        Exception? inner = null)
        : base(message, inner)
    {
        _nameOfService = nameOfService;
    }

    private UnknownResponseBodyFromRequestToServiceException(
        SerializationInfo info,
        StreamingContext context)
    {
        _nameOfService = info.GetObject<string>(nameof(_nameOfService));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(_nameOfService), _nameOfService);
    }
}