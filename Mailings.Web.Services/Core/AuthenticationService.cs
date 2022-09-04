using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.Services.Core;
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