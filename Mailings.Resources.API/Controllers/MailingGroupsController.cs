using Mailings.Resources.API.Dto;
using Mailings.Resources.API.Exceptions;
using Mailings.Resources.API.ResponseFactory;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Domain.MainModels;
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
    private readonly IHtmlMailsRepository _htmlMailsRepository;
    private readonly ITextMailsRepository _textMailsRepository;

    public MailingGroupsController(
        IMailingGroupsRepository mailingRepository, 
        IResponseFactory responseFactory, 
        IHtmlMailsRepository htmlMailsRepository,
        ITextMailsRepository textMailsRepository)
    {
        _mailingRepository = mailingRepository;
        _responseFactory = responseFactory;
        _htmlMailsRepository = htmlMailsRepository;
        _textMailsRepository = textMailsRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        Response? result = null;
        var groups = _mailingRepository.GetAll();
        var dtos = new List<MailingGroupDto>();

        foreach (var mail in groups)
            dtos.Add(ConvertToDto(mail));

        if (groups.Any())
            result = _responseFactory.CreateSuccess(result: dtos);
        else result = _responseFactory.EmptySuccess;

        return Ok(result);
    }

    [HttpGet("user-id/{userId:required}")]
    public IActionResult GetByUserId([FromRoute]string userId)
    {
        Response? result = null;
        var groups = _mailingRepository
            .GetAll()
            .Where(x => x.UserId == userId);
        var dtos = new List<MailingGroupDto>();

        foreach (var mail in groups)
            dtos.Add(ConvertToDto(mail));

        if (dtos.Any())
            result = _responseFactory.CreateSuccess(result: dtos);
        else result = _responseFactory.CreateFailedResponse(
            FailedResponseType.NotFound,
            message: TypicalTextResponses.UnknownUserIdOrMissingContentByUserId);

        return Ok(result);
    }
    [HttpGet("id/{id:required}")]
    public async Task<IActionResult> GetByMailingGroupIdAsync([FromRoute]string id)
    {
        Response? result = null;

        if (Guid.TryParse(id, out var guid))
        {
            try
            {
                var group = await _mailingRepository.GetByKeyAsync(key: guid);
                var dto = ConvertToDto(group);
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
    public async Task<IActionResult> SaveMailingInDatabase(
        [FromForm][FromBody] MailingGroupDto groupDto)
    {
        var group = await ConvertFromDtoAsync(dto: groupDto);

        var updatedEntity = await _mailingRepository
            .SaveIntoDbAsync(entity: group);
        
        var dto = ConvertToDto(updatedEntity);
        var result = _responseFactory.CreateSuccess(result: dto);

        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateMailingInDatabase(
        [FromForm][FromBody] MailingGroupDto groupDto)
    {
        var group = await ConvertFromDtoAsync(dto: groupDto);

        var updatedEntity = await _mailingRepository
            .SaveIntoDbAsync(entity: group);

        var dto = ConvertToDto(updatedEntity);
        var result = _responseFactory.CreateSuccess(result: dto);

        return Ok(result);
    }
    [HttpDelete("id/{id:required}")]
    public async Task<IActionResult> DeleteMailingInDatabase([FromRoute]string id)
    {
        Response? result = null;

        if (Guid.TryParse(id, out var guid))
        {
            try
            {
                await _mailingRepository.DeleteFromDbByKey(key: guid);
                result = _responseFactory.CreateSuccess();
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

    private async Task<MailingGroup> ConvertFromDtoAsync(MailingGroupDto dto)
    {
        var mailingGroup = new MailingGroup(dto.Name, dto.UserId)
        {
            Id = dto.Id
        };

        mailingGroup.Mail = dto.MailType switch
        {
            "Html" => await _htmlMailsRepository.GetByKeyAsync(key: dto.MailId),
            "Text" => await _textMailsRepository.GetByKeyAsync(key: dto.MailId),
            _ => throw new UnknownRequestMailTypeException()
        };

        mailingGroup.To = new List<EmailAddressTo>();
        foreach (var to in dto.To)
        {
            mailingGroup.To.Add(new EmailAddressTo()
            {
                Address = new EmailAddress()
                {
                    AddressString = to,
                    UserId = dto.UserId
                }
            });
        }

        mailingGroup.From = new EmailAddressFrom()
        {
            PseudoName = string.IsNullOrWhiteSpace(dto.SenderPseudo) ? null : dto.SenderPseudo,
        };

        return mailingGroup;
    }
    private MailingGroupDto ConvertToDto(MailingGroup mail)
        => new MailingGroupDto()
        {
            Id = mail.Id,
            MailId = mail.Mail.Id,
            MailType = mail.Mail switch
            {
                HtmlMail => Enum.GetName(MailType.Html) ?? string.Empty,
                TextMail => Enum.GetName(MailType.Text) ?? string.Empty,
                _ => throw new InvalidCastException(
                    "Error by casting mail type in mailing group")
            },
            Name = mail.Name,
            SenderPseudo = mail.From.PseudoName ?? string.Empty,
            To = mail.To.Select(c => c.Address.AddressString),
            UserId = mail.UserId,
        };
}