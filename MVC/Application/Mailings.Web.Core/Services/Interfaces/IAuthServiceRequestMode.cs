using Mailings.Web.Core.Services.Core;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Service mode of setup authenticated request
/// </summary>
public interface IAuthServiceRequestMode
{
    /// <summary>
    ///     Configure authenticated request
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <param name="serviceUri">
    ///     Service URI
    /// </param>
    /// <param name="options">
    ///     Action of authentication options
    /// </param>
    /// <returns>
    ///     Configured http request message object
    /// </returns>
    Task<HttpRequestMessage> SetupAuthRequestMessageAsync(
        ServiceRequest request,
        Uri serviceUri,
        Action<AuthenticationOptions> options);
}