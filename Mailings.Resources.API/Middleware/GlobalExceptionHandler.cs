using Mailings.Resources.API.Dto;

namespace Mailings.Resources.API.Middleware;
public class GlobalExceptionHandler : IMiddleware
{
    private readonly IResponseFactory _responseFactory;

    public GlobalExceptionHandler(IResponseFactory responseFactory)
        => _responseFactory = responseFactory;
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch
        {
            await HandleException(context);
        }
    }

    private async Task HandleException(HttpContext context)
    {
        var result = _responseFactory.EmptyInternalServerError;
        var response = context.Response;
        
        response.StatusCode = result.StatusCode;
        await response.WriteAsJsonAsync(result);
    }
}