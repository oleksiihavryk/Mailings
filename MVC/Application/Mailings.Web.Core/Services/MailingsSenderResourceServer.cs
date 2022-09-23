using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services;
/// <summary>
///     Implementation of mailing sender endpoint of resource server service
/// </summary>
public class MailingsSenderResourceService : IMailingsSenderResourceService
{
    /// <summary>
    ///     Endpoint route prefix
    /// </summary>
    public const string RoutePrefix = "/api/mailings";

    /// <summary>
    ///     Resource server service
    /// </summary>
    protected readonly ResourceService _resourceService;

    public MailingsSenderResourceService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    /// <summary>
    ///     Sending and applied a new mailing options to server
    /// </summary>
    /// <param name="requestDto">
    ///     Request data transfer object with new mailing settings
    /// </param>
    /// <returns>
    ///     Response by applying a new mailing settings
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<MailingResponse> Send(MailingRequest requestDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = requestDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingResponse>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
}