using Microsoft.AspNetCore.Http;

namespace Mailings.Web.Core.Services.Core;
/// <summary>
///     Service response with some result object
/// </summary>
/// <typeparam name="TResult">
///     Object type in response body
/// </typeparam>
public class ServiceResponse<TResult>
{
    /// <summary>
    ///     Is success status code of response
    /// </summary>
    public bool IsSuccess { get; set; }
    /// <summary>
    ///     Status code of response
    /// </summary>
    public int StatusCode { get; set; }
    /// <summary>
    ///     Body object in response
    /// </summary>
    public TResult? Result { get; set; }
    /// <summary>
    ///     Messages with response
    /// </summary>
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