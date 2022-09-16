using Mailings.Web.Core.Exceptions;
using Newtonsoft.Json;

namespace Mailings.Web.Core.Services.Core;

public abstract class BaseService : IBaseService
{
    protected readonly IAuthServiceRequestMode _serviceMode;
    protected readonly HttpClient _client;

    public Uri ServiceUri { get; }

    protected BaseService(
        Uri serviceUri,
        IHttpClientFactory httpClientFactory,
        IAuthServiceRequestMode serviceMode)
    {
        ServiceUri = serviceUri;
        _serviceMode = serviceMode;

        _client = httpClientFactory.CreateClient();
    }

    public abstract Task<HttpResponseMessage> SendAndReceiveRawAsync(
        ServiceRequest request);
    public virtual async Task<EmptyServiceResponse> SendAndReceiveEmptyResponse(
        ServiceRequest request)
    {
        var result = await SendAndReceiveRawAsync(request);

        if (result.IsSuccessStatusCode)
        {
            var responseString = await result.Content.ReadAsStringAsync();

            var response = JsonConvert
                .DeserializeObject<EmptyServiceResponse>(responseString);

            if (response != null)
                return response;
        }

        throw new UnknownServiceResponseFormatException();
    }
    public virtual async Task<ServiceResponse<TResult>> SendAndReceiveResponse<TResult>(
        ServiceRequest request)
    {
        var result = await SendAndReceiveRawAsync(request);

        if (result.IsSuccessStatusCode)
        {
            var responseString = await result.Content.ReadAsStringAsync();

            var response = JsonConvert
                .DeserializeObject<ServiceResponse<TResult>>(responseString);

            if (response != null)
                return response;
        }

        throw new UnknownServiceResponseFormatException();
    }
}