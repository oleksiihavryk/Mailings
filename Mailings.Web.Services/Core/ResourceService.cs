using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.Services.Core;

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
                    opt.ClientId = "resourceServer_Client";
                    opt.ClientSecret = "resourceServer_Secret";
                    opt.Scopes = new System.Collections.Generic.List<string>
                    {
                        "fullAccess_resourceServer"
                    };
                });

        return await _client.SendAsync(responseMessage);
    }
}