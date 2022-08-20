using Mailings.Resources.API.Exceptions;

namespace Mailings.Resources.API.ResponseFactory;

public class ResponseFactory : IResponseFactory
{
    public Response EmptySuccess => new Response()
    {
        Result = null,
        IsSuccess = true,
        Messages = Array.Empty<string>(),
        StatusCode = StatusCodes.Status204NoContent
    };
    public Response EmptyInternalServerError => new Response()
    {
        Result = null,
        IsSuccess = false,
        Messages = Array.Empty<string>(),
        StatusCode = StatusCodes.Status500InternalServerError
    };

    public virtual Response CreateSuccess(
        SuccessResponseType successType = SuccessResponseType.Ok,
        object? result = null)
        => new Response()
        {
            Result = result,
            IsSuccess = true,
            Messages = Array.Empty<string>(),
            StatusCode = successType switch
            {
                SuccessResponseType.Ok => StatusCodes.Status200OK,
                SuccessResponseType.MissingResult => StatusCodes.Status204NoContent,
                _ => throw new UnknownResponseTypeException()
            }
        };
    public virtual Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string? message = null)
        => new Response()
        {
            Result = null,
            Messages = new [] { message ?? string.Empty },
            IsSuccess = false,
            StatusCode = failedType switch
            {
                FailedResponseType.BadRequest => StatusCodes.Status400BadRequest,
                FailedResponseType.NotFound => StatusCodes.Status404NotFound,
                _ => throw new UnknownResponseTypeException()
            }
        };
    public virtual Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string[]? messages = null)
        => new Response()
        {
            Result = null,
            Messages = messages ?? Array.Empty<string>(),
            IsSuccess = false,
            StatusCode = failedType switch
            {
                FailedResponseType.BadRequest => StatusCodes.Status400BadRequest,
                FailedResponseType.NotFound => StatusCodes.Status404NotFound,
                _ => throw new UnknownResponseTypeException()
            }
        };
}