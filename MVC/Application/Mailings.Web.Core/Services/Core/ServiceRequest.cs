namespace Mailings.Web.Core.Services.Core;
/// <summary>
///     Service request model
/// </summary>
public sealed class ServiceRequest
{
    /// <summary>
    ///     Route prefix
    /// </summary>
    public string? RoutePrefix { get; set; }
    /// <summary>
    ///     Method of request
    /// </summary>
    public HttpMethod Method { get; set; }
    /// <summary>
    ///     Object in request body
    /// </summary>
    public object? BodyObject { get; set; }
    /// <summary>
    ///     Request query field
    /// </summary>
    public string? QueryField { get; set; }

    public ServiceRequest(HttpMethod method)
    {
        Method = method;
    }
}