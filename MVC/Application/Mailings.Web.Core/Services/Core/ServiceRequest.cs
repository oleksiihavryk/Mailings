namespace Mailings.Web.Core.Services.Core;

public sealed class ServiceRequest
{
    public string? RoutePrefix { get; set; }
    public HttpMethod Method { get; set; }
    public object? BodyObject { get; set; }
    public string? QueryField { get; set; }

    public ServiceRequest(HttpMethod method)
    {
        Method = method;
    }
}