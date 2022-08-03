using Mailings.Resources.API.Exceptions;

namespace Mailings.Resources.API.Dto;
public class ResponseFactory : IResponseFactory
{
    public ResponseDto EmptySuccess => new ResponseDto()
    {
        Result = null,
        IsSuccess = true,
        Messages = Array.Empty<string>(),
        StatusCode = StatusCodes.Status204NoContent
    };
    public ResponseDto EmptyInternalServerError => new ResponseDto()
    {
        Result = null,
        IsSuccess = false,
        Messages = Array.Empty<string>(),
        StatusCode = StatusCodes.Status500InternalServerError
    };

    public ResponseDto CreateSuccess(
        SuccessResponseType successType = SuccessResponseType.Ok,
        object? result = null)
        => new ResponseDto()
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
    public ResponseDto CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string? message = null)
        => new ResponseDto()
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
    public ResponseDto CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string[]? messages = null)
        => new ResponseDto()
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