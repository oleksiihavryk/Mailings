using System.Net.Http.Json;
using Mailings.Web.Services.Exceptions;

namespace Mailings.Web.Services.Core;

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

        var response = await result.Content
                           .ReadFromJsonAsync<EmptyServiceResponse>() ??
                       throw new UnknownServiceResponseFormatException();

        return response;
    }
    public virtual async Task<ServiceResponse<TResult>> SendAndReceiveResponse<TResult>(
        ServiceRequest request)
    {
        var result = await SendAndReceiveRawAsync(request);

        var response = await result.Content
                           .ReadFromJsonAsync<ServiceResponse<TResult>>() ??
                       throw new UnknownServiceResponseFormatException();

        return response;
    }
}