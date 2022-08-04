using Mailings.Resources.API.ResponseFactory;

namespace Mailings.Resources.API.Middleware;
public class GlobalExceptionHandler : IMiddleware
{
    private readonly IResponseFactory _responseFactory;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        IResponseFactory responseFactory, 
        ILogger<GlobalExceptionHandler> logger)
    {
        _responseFactory = responseFactory;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                exception: ex,
                message: "Exception is handled by global exception handler middleware");
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