namespace Mailings.Resources.API.Dto;

public interface IResponseFactory
{
    public ResponseDto EmptySuccess { get; }
    public ResponseDto EmptyInternalServerError { get; }

    public ResponseDto CreateSuccess(
        SuccessResponseType successType = SuccessResponseType.Ok,
        object? result = null);
    public ResponseDto CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string? message = null);
    public ResponseDto CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string[]? messages = null);
}