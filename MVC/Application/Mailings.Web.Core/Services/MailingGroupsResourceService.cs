using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Microsoft.AspNetCore.Http;

namespace Mailings.Web.Core.Services;
/// <summary>
///     Implementation of mailing groups endpoint of resource server service
/// </summary>
public class MailingGroupsResourceService : IMailingGroupsResourceService
{
    /// <summary>
    ///     Endpoint route prefix
    /// </summary>
    public const string RoutePrefix = "/api/mailing-groups";

    /// <summary>
    ///     Resource server service
    /// </summary>
    protected readonly ResourceService _resourceService;

    public MailingGroupsResourceService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    /// <summary>
    ///     Get all mailing groups from system
    /// </summary>
    /// <returns>
    ///     Task of async operation by receiving all mailing groups
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<IEnumerable<MailingGroup>> GetGroups()
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<MailingGroup>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    /// <summary>
    ///     Get all mailing groups by user identifier
    /// </summary>
    /// <param name="userId">
    ///     User identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by receiving all mailing groups by user identifier
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException"></exception>
    /// <exception cref="RequestToServiceIsFailedException"></exception>
    public virtual async Task<IEnumerable<MailingGroup>> GetGroupsByUserId(string userId)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/user-id/{userId}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<MailingGroup>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        if (result.StatusCode == StatusCodes.Status404NotFound)
            return Enumerable.Empty<MailingGroup>();

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    /// <summary>
    ///     Get mailing group by unique identifier
    /// </summary>
    /// <param name="id">
    ///     Mailing group identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by receiving mailing group by identifier
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    /// <exception cref="ObjectNotFoundException">
    ///     Occurred when object by current id is not found in system
    /// </exception>
    public virtual async Task<MailingGroup> GetById(string id)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/id/{id}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingGroup>(request);

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
    /// <summary>
    ///     Save mailing group in system by mailing group data transfer object
    /// </summary>
    /// <param name="mailDto">
    ///     Mailing group data transfer object
    /// </param>
    /// <returns>
    ///     Task of async operation by saving mailing group in system and receiving mailing group
    ///     data transfer object with unique identifier
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<MailingGroup> Save(MailingGroup mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingGroup>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    /// <summary>
    ///     Updating is already exist in system mailing group with data from data transfer object
    /// </summary>
    /// <param name="mailDto">
    ///     Mailing group data transfer object
    /// </param>
    /// <returns>
    ///     Data of updated mailing group object in data transfer object
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    /// <exception cref="ObjectNotFoundException">
    ///     Occurred when object by current id is not found in system
    /// </exception>
    public virtual async Task<MailingGroup> Update(MailingGroup mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Put)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<MailingGroup>(request);

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
    /// <summary>
    ///     Deleting mailing group by unique identifier 
    /// </summary>
    /// <param name="id">
    ///     Mailing group identifier
    /// </param>
    /// <returns>
    ///     Task of async operating by deleting an object by identifier
    /// </returns>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    /// <exception cref="ObjectNotFoundException">
    ///     Occurred when object by current id is not found in system
    /// </exception>
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