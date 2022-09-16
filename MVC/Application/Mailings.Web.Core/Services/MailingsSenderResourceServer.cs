using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services;

public class MailingsSenderResourceService : IMailingsSenderResourceService
{
    public const string RoutePrefix = "/api/mailings";

    protected readonly ResourceService _resourceService;

    public MailingsSenderResourceService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public virtual async Task<MailingResponseDto> Send(MailingRequestDto requestDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = requestDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingResponseDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
}