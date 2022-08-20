namespace Mailings.Authentication.API.Dto;

internal sealed class ResponseDto
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public object? Result { get; set; }
    public IEnumerable<string> Messages { get; set; }
}