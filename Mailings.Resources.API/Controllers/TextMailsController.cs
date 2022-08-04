using Mailings.Resources.API.RawDto;
using Mailings.Resources.API.ResponseFactory;
using Mailings.Resources.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/mails/text")]
//Todo: implement text mails controller later
public sealed class TextMailsController : ControllerBase
{
    private readonly ITextMailsRepository _mailsRepository;
    private readonly IResponseFactory _responseFactory;

    public TextMailsController(
        ITextMailsRepository mailsRepository,
        IResponseFactory responseFactory)
    {
        _mailsRepository = mailsRepository;
        _responseFactory = responseFactory;
    }

    [HttpGet]
    public IActionResult GetAllTextMails()
    {
        //Response? result = null;

        //var mails = _mailsRepository.GetAll();

        //if (mails.Any())
        //    result = _responseFactory.CreateSuccess(result: mails);
        //else result = _responseFactory.EmptySuccess;

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetAllTextMailsByUserId([FromRoute] string userId)
    {
        //Response? result = null;
        //var mails = _mailsRepository.GetAll();

        //mails = mails.Where(x => x.UserId == userId);

        //if (mails.Any())
        //    result = _responseFactory.CreateSuccess(result: mails);
        //else 
        //    result = _responseFactory.CreateFailedResponse(
        //        FailedResponseType.NotFound,
        //        message: TypicalTextResponses.UnknownUserIdOrMissingContentByUserId);

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetTextMailById([FromRoute] Guid id)
    {
        //Response? result = null;

        //try
        //{
        //    var textMail = await _mailsRepository.GetByKeyAsync(key: id);
        //    result = _responseFactory.CreateSuccess(result: textMail);
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
    public async Task<IActionResult> SaveTextMail(
        [FromBody][FromForm] RawMailDto rawMail)
    {
        //var mail = PrepareDto(rawMail);

        //var updatedEntity = await _mailsRepository
        //    .SaveIntoDbAsync(entity: mail);
        //var result = _responseFactory.CreateSuccess(result: updatedEntity);

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateInDatabaseTextMail(
        [FromBody][FromForm] RawMailDto rawMail)
    {
        //var mail = PrepareDto(rawMail);

        //var updatedEntity = await _mailsRepository
        //    .SaveIntoDbAsync(entity: mail);
        //var result = _responseFactory.CreateSuccess(result: updatedEntity);

        //return Ok(result);
        throw new NotImplementedException();
    }
    [HttpDelete("id/{id:guid}")]
    public async Task<IActionResult> DeleteMail(
        [FromRoute] Guid id)
    {
        //Response? result = null;

        //try
        //{
        //    await _mailsRepository.DeleteFromDbByKey(key: id);
        //    result = _responseFactory.EmptySuccess;
        //}
        //catch (ObjectNotFoundInDatabaseException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        FailedResponseType.NotFound,
        //        message: TypicalTextResponses.EntityNotFoundById);
        //}

        //return Ok(result);
        throw new NotImplementedException();
    }
}