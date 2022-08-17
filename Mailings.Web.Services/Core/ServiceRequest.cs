namespace Mailings.Web.Services.Core;

public class ServiceRequest
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