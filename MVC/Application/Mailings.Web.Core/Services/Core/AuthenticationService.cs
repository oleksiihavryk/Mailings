using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.Core.Services.Core;
/// <summary>
///     Service which make a request to authentication service
/// </summary>
public class AuthenticationService : BaseService
{
    public AuthenticationService( 
        IHttpClientFactory httpClientFactory,
        IAuthServiceRequestMode serviceMode) 
        : base(
            new Uri(Servers.Authentication), 
            httpClientFactory, serviceMode)
    {
    }

    /// <summary>
    ///     Sending service request to authentication service and receive raw response
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Http response message object
    /// </returns>
    public override async Task<HttpResponseMessage> SendAndReceiveRawAsync(
        ServiceRequest request)
    {
        var responseMessage = await _serviceMode
            .SetupAuthRequestMessageAsync(
                request,
                ServiceUri,
                options: opt =>
                {
                    opt.ClientId = IdentityPrivateData.Authentication.ClientId;
                    opt.ClientSecret = IdentityPrivateData.Authentication.ClientSecret;
                    opt.Scopes = IdentityPrivateData.Authentication.Scopes;
                });

        return await _client.SendAsync(responseMessage);
    }
}