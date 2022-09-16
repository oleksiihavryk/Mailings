using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.Core.Services.Core;

public class ResourceService : BaseService
{
    public ResourceService(
        IHttpClientFactory httpClientFactory,
        IAuthServiceRequestMode serviceMode)
        : base(
            new Uri(Servers.Resources),
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
                    opt.ClientId = IdentityPrivateData.Resources.ClientId;
                    opt.ClientSecret = IdentityPrivateData.Resources.ClientSecret;
                    opt.Scopes = IdentityPrivateData.Resources.Scopes;
                });

        return await _client.SendAsync(responseMessage);
    }
}