namespace Mailings.Web.Core.Services.Core;
/// <summary>
///     Service response object with not object in body
/// </summary>
public class EmptyServiceResponse : ServiceResponse<object>
{
    public EmptyServiceResponse()
    {
    }
    public EmptyServiceResponse(
        bool isSuccess,
        int statusCode,
        IEnumerable<string> messages)  
        : base(isSuccess, statusCode, result: null, messages)
    {
    }
}