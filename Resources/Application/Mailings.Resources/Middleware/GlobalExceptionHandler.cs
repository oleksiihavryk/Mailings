using Mailings.Resources.Core.ResponseFactory;

namespace Mailings.Resources.Middleware;
internal class GlobalExceptionHandler : IMiddleware
{
    protected readonly IResponseFactory _responseFactory;
    protected readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        IResponseFactory responseFactory, 
        ILogger<GlobalExceptionHandler> logger)
    {
        _responseFactory = responseFactory;
        _logger = logger;
    }

    public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next)
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

    protected virtual async Task HandleException(HttpContext context)
    {
        var result = _responseFactory.EmptyInternalServerError;
        var response = context.Response;
        
        response.StatusCode = result.StatusCode;
        await response.WriteAsJsonAsync(result);
    }
}