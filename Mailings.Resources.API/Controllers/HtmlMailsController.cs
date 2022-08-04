using System.Text;
using Mailings.Resources.API.RawDto;
using Mailings.Resources.API.ResponseFactory;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;
[ApiController]
[Authorize]
[Route("/api/mails/html")]
public sealed class HtmlMailsController : ControllerBase
{
    private readonly IHtmlMailsRepository _htmlMailsRepository;
    private readonly IResponseFactory _responseFactory;

    public HtmlMailsController(
        IHtmlMailsRepository htmlMailsRepository, 
        IResponseFactory responseFactory)
    {
        _htmlMailsRepository = htmlMailsRepository;
        _responseFactory = responseFactory;
    }

    [HttpGet]
    public IActionResult GetAllHtmlMails()
    {
        Response? result = null;
        var mails = _htmlMailsRepository.GetAll();

        if (mails.Any())
            result = _responseFactory.CreateSuccess(result: mails);
        else result = _responseFactory.EmptySuccess;

        return Ok(result);
    }
    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetAllHtmlMailsByUserId([FromRoute]string userId)
    {
        Response? result = null;
        var mails = _htmlMailsRepository.GetAll();
        
        mails = mails.Where(x => x.UserId == userId);

        if (mails.Any())
            result = _responseFactory.CreateSuccess(result: mails);
        else result = _responseFactory.CreateFailedResponse(
            FailedResponseType.NotFound,
            message: TypicalTextResponses.UnknownUserIdOrMissingContentByUserId);

        return Ok(result);
    }
    [HttpGet("id/{id:guid}")]
    public async Task<IActionResult> GetHtmlMailById([FromRoute]Guid id)
    {
        Response? result = null;

        try
        {
            var htmlMail = await _htmlMailsRepository.GetByKeyAsync(key: id);
            result = _responseFactory.CreateSuccess(result: htmlMail);
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
    public async Task<IActionResult> SaveHtmlMail(
        [FromBody][FromForm] RawMailDto rawMail)
    {
        HtmlMailDto mail = PrepareDto(rawMail);

        var updatedEntity = await _htmlMailsRepository
            .SaveIntoDbAsync(entity: mail);
        var result = _responseFactory.CreateSuccess(result: updatedEntity);

        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateInDatabaseHtmlMail(
        [FromBody][FromForm] RawMailDto rawMail)
    {
        HtmlMailDto mail = PrepareDto(rawMail);

        var updatedEntity = await _htmlMailsRepository
            .SaveIntoDbAsync(entity: mail);
        var result = _responseFactory.CreateSuccess(result: updatedEntity);

        return Ok(result);
    }
    [HttpDelete("id/{id:guid}")]
    public async Task<IActionResult> DeleteMail(
        [FromRoute] Guid id)
    {
        Response? result = null;

        try
        {
            await _htmlMailsRepository.DeleteFromDbByKey(key: id);
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

    private HtmlMailDto PrepareDto(RawMailDto rawData)
    {
        var htmlDto = new HtmlMailDto(
            theme: rawData.Theme,
            userId: rawData.UserId)
        {
            ByteContent = Encoding.UTF8.GetBytes(rawData.Content),
            Id = rawData.Id ?? Guid.Empty
        };

        return htmlDto;
    }
}