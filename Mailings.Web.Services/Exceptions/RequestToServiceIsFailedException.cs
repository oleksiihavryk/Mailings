using System.Collections;

namespace Mailings.Web.Services.Exceptions;

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
}