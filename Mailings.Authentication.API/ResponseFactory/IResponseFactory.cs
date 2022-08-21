namespace Mailings.Authentication.API.ResponseFactory;

public interface IResponseFactory
{
    public Response EmptySuccess { get; }
    public Response EmptyInternalServerError { get; }

    public Response CreateSuccess(
        SuccessResponseType successType = SuccessResponseType.Ok,
        object? result = null);
    public Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string? message = null);
    public Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string[]? messages = null);
}