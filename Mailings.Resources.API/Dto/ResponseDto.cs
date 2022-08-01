namespace Mailings.Resources.API.Dto;
public class ResponseDto
{
    public bool IsSuccess { get; set; } = false;
    public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
    public object? Result { get; set; } = null;
    public IEnumerable<string> Messages { get; set; } =
        Array.Empty<string>();
}