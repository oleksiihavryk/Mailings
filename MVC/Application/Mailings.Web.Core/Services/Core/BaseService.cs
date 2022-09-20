using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Interfaces;
using Newtonsoft.Json;

namespace Mailings.Web.Core.Services.Core;

/// <summary>
///     Base service for making requests to other services 
/// </summary>
public abstract class BaseService : IBaseService
{
    /// <summary>
    ///     Authentication service request mode
    /// </summary>
    protected readonly IAuthServiceRequestMode _serviceMode;
    /// <summary>
    ///     Http client
    /// </summary>
    protected readonly HttpClient _client;

    /// <summary>
    ///     Service base URI
    /// </summary>
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

    /// <summary>
    ///     Sending service request and receive raw data
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Http response message object
    /// </returns>
    public abstract Task<HttpResponseMessage> SendAndReceiveRawAsync(
        ServiceRequest request);
    /// <summary>
    ///     Sending service request and receive processed empty service response
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Empty service response
    /// </returns>
    /// <exception cref="UnknownServiceResponseFormatException">
    ///     Occurred when response format from service is unhandled by this method
    /// </exception>
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
    /// <summary>
    ///     Sending service request and receive processed
    ///     service response with contained object in body
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Service response with contained object in body
    /// </returns>
    /// <exception cref="UnknownServiceResponseFormatException">
    ///     Occurred when response format from service is unhandled by this method
    /// </exception>
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