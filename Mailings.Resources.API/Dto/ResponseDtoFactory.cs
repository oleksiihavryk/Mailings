namespace Mailings.Resources.API.Dto;
public class ResponseFactory : IResponseFactory
{
    public ResponseDto EmptySuccess { get; }
    public ResponseDto EmptyInternalServerError { get; }

    public ResponseDto CreateSuccess(object? result = null)
    {
        throw new NotImplementedException();
    }
    public ResponseDto CreateFailedResponse(int statusCode, string message)
    {
        throw new NotImplementedException();
    }
    public ResponseDto CreateFailedResponse(int statusCode, string[] message)
    {
        throw new NotImplementedException();
    }
}