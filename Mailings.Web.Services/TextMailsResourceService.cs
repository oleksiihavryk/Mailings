using Mailings.Web.Services.Core;
using Mailings.Web.Services.Exceptions;
using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public class TextMailsResourceService : ITextMailsResourceService
{
    protected const string RoutePrefix = "/api/mails/html";

    protected readonly ResourceService _resourceService;

    public TextMailsResourceService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public async Task<IEnumerable<MailDto>> GetMails()
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<MailDto>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public async Task<IEnumerable<MailDto>> GetMailsByUserId(string userId)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/userId/{userId}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<MailDto>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public async Task<MailDto> GetById(string id)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/id/{id}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public async Task<MailDto> Save(MailDto mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public async Task<MailDto> Update(MailDto mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public async Task Delete(string id)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix + $"/id/{id}",
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveEmptyResponse(request);

        //return result
        if (!result.IsSuccess)
            throw new RequestToServiceIsFailedException(
                nameOfService: nameof(ResourceService));
    }
}