using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.Dto;
using Microsoft.AspNetCore.Http;

namespace Mailings.Web.Core.Services;

public class MailingGroupsResourceService : IMailingGroupsResourceService
{
    public const string RoutePrefix = "/api/mailing-groups";

    protected readonly ResourceService _resourceService;

    public MailingGroupsResourceService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public virtual async Task<IEnumerable<MailingGroupDto>> GetGroups()
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<MailingGroupDto>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public virtual async Task<IEnumerable<MailingGroupDto>> GetGroupsByUserId(string userId)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/user-id/{userId}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<MailingGroupDto>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        if (result.StatusCode == StatusCodes.Status404NotFound)
            return Enumerable.Empty<MailingGroupDto>();

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public virtual async Task<MailingGroupDto> GetById(string id)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/id/{id}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingGroupDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        if (result.StatusCode == StatusCodes.Status404NotFound)
            throw new ObjectNotFoundException();

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public virtual async Task<MailingGroupDto> Save(MailingGroupDto mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingGroupDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public virtual async Task<MailingGroupDto> Update(MailingGroupDto mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Put)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingGroupDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        if (result.StatusCode == StatusCodes.Status404NotFound)
            throw new ObjectNotFoundException();

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    public virtual async Task Delete(string id)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Delete)
        {
            RoutePrefix = RoutePrefix + $"/id/{id}",
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveEmptyResponse(request);

        if (result.StatusCode == StatusCodes.Status404NotFound)
            throw new ObjectNotFoundException();

        //return result
        if (!result.IsSuccess)
            throw new RequestToServiceIsFailedException(
                nameOfService: nameof(ResourceService));
    }
}