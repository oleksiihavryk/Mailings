namespace Mailings.Authentication.API.Dto;

internal sealed class ResponseDto
{
    public bool IsSuccess { get; set; } = false;
    public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
    public object? Result { get; set; } = null;
    public IEnumerable<string> Messages { get; set; } = Enumerable.Empty<string>();
}