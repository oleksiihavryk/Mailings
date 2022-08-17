namespace Mailings.Web.Services.Core;
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