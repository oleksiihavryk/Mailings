using Microsoft.AspNetCore.Http;

namespace Mailings.Web.Services.Core;

public class ServiceResponse<TResult>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public TResult? Result { get; set; }
    public IEnumerable<string> Messages { get; set; }

    public ServiceResponse()
        : this(
            isSuccess: false,
            statusCode: StatusCodes.Status500InternalServerError,
            result: default,
            messages: Array.Empty<string>())
    {
    }
    public ServiceResponse(
        bool isSuccess,
        int statusCode, 
        TResult? result, 
        IEnumerable<string> messages)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Result = result;
        Messages = messages;
    }
}