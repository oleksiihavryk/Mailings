namespace Mailings.Resources.API.Dto;

public interface IResponseFactory
{
    public ResponseDto EmptySuccess { get; }
    public ResponseDto EmptyInternalServerError { get; }

    public ResponseDto CreateSuccess(object? result = null);
    public ResponseDto CreateFailedResponse(int statusCode, string message);
    public ResponseDto CreateFailedResponse(int statusCode, string[] message);
}