using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Microsoft.AspNetCore.Http;

namespace Mailings.Web.Core.Services;
/// <summary>
///     Implementation of text mails endpoint of resource server service
/// </summary>
public class TextMailsResourceService : ITextMailsResourceService
{
    /// <summary>
    ///     Endpoint route prefix
    /// </summary>
    public const string RoutePrefix = "/api/mails/text";

    /// <summary>
    ///     Resource server service
    /// </summary>
    protected readonly ResourceService _resourceService;

    public TextMailsResourceService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    /// <summary>
    ///     Get all mails from resource endpoint
    /// </summary>
    /// <returns>
    ///     All mails from endpoint
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<IEnumerable<Mail>> GetMails()
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<Mail>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    /// <summary>
    ///     Get all mails by identifier of current user
    /// </summary>
    /// <param name="userId">
    ///     User identifier
    /// </param>
    /// <returns>
    ///     All mails of user
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<IEnumerable<Mail>> GetMailsByUserId(string userId)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/user-id/{userId}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<IEnumerable<Mail>>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        if (result.StatusCode == StatusCodes.Status404NotFound)
            return Enumerable.Empty<Mail>();

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    /// <summary>
    ///     Get mail by identifier
    /// </summary>
    /// <param name="id">
    ///     Object identifier
    /// </param>
    /// <returns>
    ///     Object with current identifier
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
    public virtual async Task<Mail> GetById(string id)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Get)
        {
            RoutePrefix = RoutePrefix + $"/id/{id}"
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<Mail>(request);

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
    ///     Save mail of current format into resource server
    /// </summary>
    /// <param name="mailDto">
    ///     Data transfer object of current mail
    /// </param>
    /// <returns>
    ///     Task of async operation by saving mail data transfer object and
    ///     returning saved object with identifier
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<Mail> Save(Mail mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<Mail>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(ResourceService));

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(ResourceService));
    }
    /// <summary>
    ///     Updating mail in system by data transfer object
    /// </summary>
    /// <param name="mailDto">
    ///     Data transfer object of mail
    /// </param>
    /// <returns>
    ///     Task of async operation by updating mail object
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    /// <exception cref="ObjectNotFoundException">
    ///     Object for updating is not found in system
    /// </exception>
    public virtual async Task<Mail> Update(Mail mailDto)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Put)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = mailDto
        };

        //send request
        var result = await _resourceService
            .SendAndReceiveResponse<Mail>(request);

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
    ///     Deleting mail with current identifier from system
    /// </summary>
    /// <param name="id">
    ///     Mail identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by deleting an mail with current identifier from system
    /// </returns>
    /// <exception cref="ObjectNotFoundException">
    ///     Object not found in system by current identifier
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
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