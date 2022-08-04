using AutoMapper;
using Mailings.Resources.API.RawDto;
using Mailings.Resources.API.ResponseFactory;
using Mailings.Resources.Application.MailingService;
using Mailings.Resources.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
//Todo: implement main mailings controller later
public class MailingsController : ControllerBase
{
    private readonly IMailingService _mailingService;
    private readonly IMailingGroupsRepository _mailingGroups;
    private readonly IResponseFactory _responseFactory;
    private readonly IMapper _mapper;

    public MailingsController(
        IMailingService mailingService, 
        IMailingGroupsRepository mailingGroups,
        IResponseFactory responseFactory,
        IMapper mapper)
    {
        _mailingService = mailingService;
        _mailingGroups = mailingGroups;
        _responseFactory = responseFactory;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> SendMailingAsync(
        [FromBody][FromForm] MailingSendRequestDto sendRequestDto)
    {
        //Response? result = null;

        //try
        //{
        //    var sendRequest = await PrepareMailingSendRequestAsync(sendRequestDto);
        //    var response = await _mailingService.SendAsync(sendRequest);

        //    result = _responseFactory.CreateSuccess(result: response);
        //}
        //catch (ObjectNotFoundInDatabaseException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        failedType: FailedResponseType.BadRequest,
        //        message: "Mailing group with current id is not found in database");
        //}
        //catch (UnknownRequestMailTypeException)
        //{
        //    result = _responseFactory.CreateFailedResponse(
        //        failedType: FailedResponseType.BadRequest,
        //        message: "Unknown type of mail sending.");
        //}

        //return Ok(result);
        throw new NotImplementedException();
    }
}