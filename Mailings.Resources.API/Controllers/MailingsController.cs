using Mailings.Resources.API.Dto;
using Mailings.Resources.API.Exceptions;
using Mailings.Resources.API.ResponseFactory;
using Mailings.Resources.Application.MailingService;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Domain.MainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class MailingsController : ControllerBase
{
    private readonly IMailingService _mailingService;
    private readonly IMailingGroupsRepository _mailingGroups;
    private readonly IResponseFactory _responseFactory;
    private readonly IHistoryNotesRepository _history;

    public MailingsController(
        IMailingService mailingService, 
        IMailingGroupsRepository mailingGroups,
        IResponseFactory responseFactory, 
        IHistoryNotesRepository history)
    {
        _mailingService = mailingService;
        _mailingGroups = mailingGroups;
        _responseFactory = responseFactory;
        _history = history;
    }

    [HttpPost]
    public async Task<IActionResult> SendMailingAsync(
        [FromBody][FromForm] MailingRequestDto sendRequestDto)
    {
        Response? result = null;
        
        try
        {
            var sendRequest = await PrepareMailingSendRequestAsync(sendRequestDto);
            var response = await _mailingService.SendAsync(sendRequest);
            await WriteMailingHistoryAsync(response.GetHistoryNote());
            var dtoResult = CreateDtoFromResponse(response);

            result = _responseFactory.CreateSuccess(result: dtoResult);
        }
        catch (ObjectNotFoundInDatabaseException)
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.BadRequest,
                message: "Mailing group with current id is not found in database");
        }
        catch (UnknownRequestMailTypeException)
        {
            result = _responseFactory.CreateFailedResponse(
                failedType: FailedResponseType.BadRequest,
                message: "Unknown type of mail sending.");
        }

        return Ok(result);
    }

    private MailingResponseDto CreateDtoFromResponse(MailingSendResponse response)
    {
        return new MailingResponseDto()
        {
            IsSuccess = response.IsSuccess,
            MailingId = response.Group.Id,
        };
    }
    private async Task<MailingSendRequest> PrepareMailingSendRequestAsync(
        MailingRequestDto sendRequestDto)
    {
        var group = await _mailingGroups.GetByIdAsync(key: sendRequestDto.MailingId);
        var type = sendRequestDto.SendType.ToUpper().Trim(' ') switch
        {
            "HTML" => MailType.Html,
            "TEXT" => MailType.Text,
            _ => throw new UnknownRequestMailTypeException()
        };

        var mailingSendRequest = new MailingSendRequest()
        {
            Group = group,
            MailType = type
        };

        return mailingSendRequest;
    }
    private async Task WriteMailingHistoryAsync(HistoryNoteMailingGroup historyNote)
        => await _history.SaveAsync(entity: historyNote);
}