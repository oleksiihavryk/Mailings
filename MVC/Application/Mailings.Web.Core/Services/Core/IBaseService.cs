namespace Mailings.Web.Core.Services.Core;

internal interface IBaseService
{
    Uri ServiceUri { get; }

    Task<HttpResponseMessage> SendAndReceiveRawAsync(ServiceRequest message);
    Task<EmptyServiceResponse> SendAndReceiveEmptyResponse(
        ServiceRequest request);
    Task<ServiceResponse<TResult>> SendAndReceiveResponse<TResult>(
        ServiceRequest request);
}