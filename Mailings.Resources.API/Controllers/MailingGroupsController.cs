using Mailings.Resources.API.RawDto;
using Mailings.Resources.API.ResponseFactory;
using Mailings.Resources.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;

[ApiController]
[Authorize]
[Route("api/mailing-groups")]
//Todo: implement mailing groups controller later
public class MailingGroupsController : ControllerBase
{
    private readonly IMailingGroupsRepository _mailingRepository;
    private readonly IResponseFactory _responseFactory;
    private readonly IHtmlMailsRepository _htmlMails;
    private readonly ITextMailsRepository _textMails;

    public MailingGroupsController(
        IMailingGroupsRepository mailingRepository, 
        IResponseFactory responseFactory, 
        IHtmlMailsRepository htmlMails,
        ITextMailsRepository textMails)
    {
        _mailingRepository = mailingRepository;
        _responseFactory = responseFactory;
        _htmlMails = htmlMails;
        _textMails = textMails;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        //Response? result = null;
        //var groups = _mailingRepository.GetAll();

        //if (groups.Any())
        //    result = _responseFactory.CreateSuccess(result: groups);
        //else result = _responseFactory.EmptySuccess;

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetByUserId([FromRoute]string userId)
    {
        //Response? result = null;
        //var groups = _mailingRepository.GetAll();

        //groups = groups.Where(mail => mail.UserId == userId);

        //if (groups.Any())
        //    result = _responseFactory.CreateSuccess(result: groups);
        //else result = _responseFactory.CreateFailedResponse(
        //    failedType: FailedResponseType.NotFound,
        //    message: TypicalTextResponses.UnknownUserIdOrMissingContentByUserId);

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetByMailingGroupIdAsync(Guid id)
    {
        //Response? result = null;

        //try
        //{
        //    var group = await _mailingRepository.GetByKeyAsync(key: id);
        //    result = _responseFactory.CreateSuccess(result: group);
        //}
        //catch (ObjectNotFoundInDatabaseException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        failedType: FailedResponseType.NotFound,
        //        message: TypicalTextResponses.EntityNotFoundById);
        //}

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpPost]
    public async Task<IActionResult> SaveMailingInDatabase(
        [FromForm][FromBody] RawMailingGroupDto unpreparedMailingGroup)
    {
        //Response? result = null;

        //try
        //{
        //    var mailingGroup = await PrepareDtoAsync(unpreparedMailingGroup);

        //    var savedMailingGroup = await _mailingRepository
        //        .SaveIntoDbAsync(mailingGroup);
        //    try
        //    {
        //        result = _responseFactory
        //            .CreateSuccess(result: savedMailingGroup);
        //    }
        //    catch
        //    {
        //        await _mailingRepository.DeleteFromDbByKey(savedMailingGroup.Id);
        //        throw;
        //    }
        //}
        //catch (UnknownRequestMailTypeException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        failedType: FailedResponseType.BadRequest,
        //        message: "Unknown mail type.");
        //}
        //catch (ObjectNotFoundInDatabaseException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        FailedResponseType.BadRequest,
        //        message: "Mail with current id and mail type is not found in database.");
        //}

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateMailingInDatabase(
        [FromForm][FromBody] RawMailingGroupDto unpreparedMailingGroup)
    {
        //Response? result = null;

        //try
        //{
        //    var mailingGroup = await PrepareDtoAsync(unpreparedMailingGroup);
        //    var savedMailingGroup = await _mailingRepository
        //        .SaveIntoDbAsync(mailingGroup);

        //    result = _responseFactory
        //        .CreateSuccess(result: savedMailingGroup);
        //}
        //catch (UnknownRequestMailTypeException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        failedType: FailedResponseType.BadRequest,
        //        message: "Unknown mail type.");
        //}
        //catch (ObjectNotFoundInDatabaseException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        FailedResponseType.BadRequest,
        //        message: "Mail with current id and mail type is not found in database.");
        //}

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpDelete("id/{id:guid}")]
    public async Task<IActionResult> DeleteMailingInDatabase(Guid id)
    {
        //Response? result = null;

        //try
        //{
        //    await _mailingRepository.DeleteFromDbByKey(key: id);
        //    result = _responseFactory.EmptySuccess;
        //}
        //catch (ObjectNotFoundInDatabaseException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        failedType: FailedResponseType.NotFound,
        //        message: TypicalTextResponses.EntityNotFoundById);
        //}

        //return Ok(result);
        throw new NotImplementedException();
    }
}