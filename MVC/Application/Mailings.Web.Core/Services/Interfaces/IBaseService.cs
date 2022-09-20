using Mailings.Web.Core.Services.Core;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Base service interface for making request to other services (applications)
/// </summary>
internal interface IBaseService
{
    /// <summary>
    ///     Service URI
    /// </summary>
    Uri ServiceUri { get; }

    /// <summary>
    ///     Send service request and receive raw http response message
    /// </summary>
    /// <param name="message">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Http response message object
    /// </returns>
    Task<HttpResponseMessage> SendAndReceiveRawAsync(ServiceRequest message);
    /// <summary>
    ///     Send service request and receive processed service response with no object in body
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Service response without object in body
    /// </returns>
    Task<EmptyServiceResponse> SendAndReceiveEmptyResponse(
        ServiceRequest request);
    /// <summary>
    ///     Sending service request and receive service response 
    /// </summary>
    /// <typeparam name="TResult">Type of object in body</typeparam>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <returns>
    ///     Service response with object in body
    /// </returns>
    Task<ServiceResponse<TResult>> SendAndReceiveResponse<TResult>(
        ServiceRequest request);
}