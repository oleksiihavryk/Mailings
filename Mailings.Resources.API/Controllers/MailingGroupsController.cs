using Mailings.Resources.API.Dto;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;

[ApiController]
[Authorize]
[Route("api/mailing-groups")]
public class MailingGroupsController : ControllerBase
{
    private readonly IMailingGroupsRepository _mailingRepository;
    private readonly IResponseFactory _responseFactory;

    public MailingGroupsController(
        IMailingGroupsRepository mailingRepository, 
        IResponseFactory responseFactory)
    {
        _mailingRepository = mailingRepository;
        _responseFactory = responseFactory;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        ResponseDto? result = null;
        var groups = _mailingRepository.GetAll();

        if (groups.Any())
            result = _responseFactory.CreateSuccess(result: groups);
        else result = _responseFactory.EmptySuccess;

        return Ok(result);
    }
    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetByUserId([FromRoute]string userId)
    {
        ResponseDto? result = null;
        var groups = _mailingRepository.GetAll();

        groups = groups.Where(mail => mail.UserId == userId);

        if (groups.Any())
            result = _responseFactory.CreateSuccess(result: groups);
        else result = _responseFactory.CreateFailedResponse(
            failedType: FailedResponseType.NotFound,
            message: TypicalTextResponses.UnknownUserIdOrMissingContentByUserId);

        return Ok(result);
    }
    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetByMailingGroupIdAsync(Guid id)
    {
        ResponseDto? result = null;

        try
        {
            var group = await _mailingRepository.GetByKeyAsync(key: id);
            result = _responseFactory.CreateSuccess(result: group);
        }
        catch (ObjectNotFoundInDatabaseException)
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.NotFound,
                message: TypicalTextResponses.EntityNotFoundById);
        }

        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> SaveMailingInDatabase(
        [FromForm][FromBody] UnpreparedMailingGroupDto unpreparedMailingGroup)
    {
        var mailingGroup = CreatePreparedMailingGroupDto(unpreparedMailingGroup);

        var savedMailingGroup = await _mailingRepository
            .SaveIntoDbAsync(mailingGroup);
        try
        {
            var result = _responseFactory
                .CreateSuccess(result: savedMailingGroup);
            
            return Ok(result);
        }
        catch
        {
            await _mailingRepository.DeleteFromDbByKey(savedMailingGroup.Id);
            throw;
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateMailingInDatabase(
        [FromForm][FromBody] UnpreparedMailingGroupDto unpreparedMailingGroup)
    {
        var mailingGroup = CreatePreparedMailingGroupDto(unpreparedMailingGroup);

        var savedMailingGroup = await _mailingRepository
            .SaveIntoDbAsync(mailingGroup);
        
        var result = _responseFactory
            .CreateSuccess(result: savedMailingGroup);

        return Ok(result);
    }
    [HttpDelete("id/{id:guid}")]
    public async Task<IActionResult> DeleteMailingInDatabase(Guid id)
    {
        ResponseDto? result = null;

        try
        {
            await _mailingRepository.DeleteFromDbByKey(key: id);
            result = _responseFactory.EmptySuccess;
        }
        catch (ObjectNotFoundInDatabaseException)
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.NotFound,
                message: TypicalTextResponses.EntityNotFoundById);
        }

        return Ok(result);
    }

    private MailingGroupDto CreatePreparedMailingGroupDto(
        UnpreparedMailingGroupDto unpreparedMailingGroup)
    {
        var mailingGroup = new MailingGroupDto()
        {
            Name = unpreparedMailingGroup.Name,
            UserId = unpreparedMailingGroup.UserId,
            Id = unpreparedMailingGroup.Id
        };

        mailingGroup.From = new EmailAddressFromDto()
        {
            Group = mailingGroup,
            Address = new EmailAddressDto
            {
                Id = unpreparedMailingGroup.From.AddressId,
                Address = unpreparedMailingGroup.From.Address
            },
            Id = unpreparedMailingGroup.From.Id
        };

        foreach (var to in unpreparedMailingGroup.To)
        {
            mailingGroup.To.Add(new EmailAddressToDto()
            {
                Group = mailingGroup,
                Address = new EmailAddressDto()
                {
                    Id = to.AddressId,
                    Address = to.Address,
                }, 
                Id = to.Id
            });
        }

        return mailingGroup;
    }
}