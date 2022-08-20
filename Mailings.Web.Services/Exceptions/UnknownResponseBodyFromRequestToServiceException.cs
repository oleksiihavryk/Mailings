using System.Collections;

namespace Mailings.Web.Services.Exceptions;

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
}