namespace Mailings.Authentication.API.Dto;
public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public object? Result { get; set; }
    public IEnumerable<string> Messages { get; set; }
}