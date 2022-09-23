using Mailings.Resources.Core.ResponseFactory;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Domain.Models;
using Mailings.Resources.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.Controllers;
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
        var dtos = new List<MailDto>();

        foreach (var mail in mails)
            dtos.Add(ConvertToDto(mail));

        if (mails.Any())
            result = _responseFactory.CreateSuccess(result: dtos);
        else result = _responseFactory.EmptySuccess;

        return Ok(result);
    }
    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetAllHtmlMailsByUserId([FromRoute]string userId)
    {
        Response? result = null;
        var mails = _htmlMailsRepository
            .GetAll()
            .Where(x => x.UserId == userId);
        var dtos = new List<MailDto>();

        foreach (var mail in mails)
            dtos.Add(ConvertToDto(mail));

        if (mails.Any())
            result = _responseFactory.CreateSuccess(result: dtos);
        else result = _responseFactory.CreateFailedResponse(
            FailedResponseType.NotFound,
            message: TypicalTextResponses.UnknownUserIdOrMissingContentByUserId);

        return Ok(result);
    }
    [HttpGet("id/{id:required}")]
    public async Task<IActionResult> GetHtmlMailById([FromRoute]string id)
    {
        Response? result = null;

        if (Guid.TryParse(id, out var guid))
        {
            try
            {
                var htmlMail = await _htmlMailsRepository.GetByIdAsync(key: guid);
                var dto = ConvertToDto(htmlMail);
                result = _responseFactory.CreateSuccess(result: dto);
            }
            catch (ObjectNotFoundInDatabaseException)
            {
                result = _responseFactory.CreateFailedResponse(
                    failedType: FailedResponseType.NotFound,
                    message: TypicalTextResponses.EntityNotFoundById);
            }
        }
        else
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.BadRequest,
                message: TypicalTextResponses.IncorrectClientInput);
        }

        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> SaveHtmlMail(
        [FromBody] MailDto mailDto)
    {
        var mail = ConvertFromDto(dto: mailDto);

        var updatedEntity = await _htmlMailsRepository
            .SaveAsync(entity: mail);

        var dto = ConvertToDto(updatedEntity);
        var result = _responseFactory.CreateSuccess(result: dto);

        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateInDatabaseHtmlMail(
        [FromBody] MailDto mailDto)
    {
        Response? result = null;
        try
        {
            var mail = ConvertFromDto(dto: mailDto);

            var updatedEntity = await _htmlMailsRepository
                .UpdateAsync(entity: mail);
            var dto = ConvertToDto(updatedEntity);

            result = _responseFactory.CreateSuccess(result: dto);
        }
        catch (ObjectNotFoundInDatabaseException)
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.NotFound,
                message: TypicalTextResponses.EntityNotFoundById);
        }
        return Ok(result);
    }
    [HttpDelete("id/{id:guid}")]
    public async Task<IActionResult> DeleteMail(
        [FromRoute] Guid id)
    {
        Response? result = null;

        try
        {
            await _htmlMailsRepository.DeleteByIdAsync(key: id);
            result = _responseFactory.CreateSuccess();
        }
        catch (ObjectNotFoundInDatabaseException)
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.NotFound,
                message: TypicalTextResponses.EntityNotFoundById);
        }

        return Ok(result);
    }

    private HtmlMail ConvertFromDto(MailDto dto) => (HtmlMail)dto;
    private MailDto ConvertToDto(HtmlMail htmlMail) => (MailDto)htmlMail;
}