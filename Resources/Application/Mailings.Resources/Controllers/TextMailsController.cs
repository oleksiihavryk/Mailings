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
[Route("/api/mails/text")]
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
        Response? result = null;
        var mails = _mailsRepository.GetAll();
        var dtos = new List<MailDto>();

        foreach (var mail in mails)
            dtos.Add(ConvertToDto(mail));

        if (mails.Any())
            result = _responseFactory.CreateSuccess(result: dtos);
        else result = _responseFactory.EmptySuccess;

        return Ok(result);
    }
    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetAllTextMailsByUserId([FromRoute] string userId)
    {
        Response? result = null;
        var mails = _mailsRepository
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
    public async Task<IActionResult> GetTextMailById([FromRoute] string id)
    {
        Response? result = null;

        if (Guid.TryParse(id, out var guid))
        {
            try
            {
                var htmlMail = await _mailsRepository.GetByIdAsync(key: guid);
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
    public async Task<IActionResult> SaveTextMail(
        [FromBody] MailDto mailDto)
    {
        var mail = ConvertFromDto(dto: mailDto);

        var updatedEntity = await _mailsRepository
            .SaveAsync(entity: mail);
        var dto = ConvertToDto(updatedEntity);
        var result = _responseFactory.CreateSuccess(result: dto);

        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateInDatabaseTextMail(
        [FromBody] MailDto mailDto)
    {
        Response? result = null;
        try
        {
            var mail = ConvertFromDto(dto: mailDto);

            var updatedEntity = await _mailsRepository
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
            await _mailsRepository.DeleteByIdAsync(key: id);
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

    private MailDto ConvertToDto(TextMail mail) => (MailDto)mail;
    private TextMail ConvertFromDto(MailDto dto) => (TextMail)dto;
}