using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.Dto;
using Mailings.Web.Filters;
using Mailings.Web.Shared.SystemConstants;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class SenderController : Controller
{
    private readonly IMailingsSenderResourceService _senderResourceService;

    public SenderController(IMailingsSenderResourceService senderResourceService)
    {
        _senderResourceService = senderResourceService;
    }

    [HttpGet]
    [ServiceFilter(typeof(MailingsUserSecuredServiceFilter))]
    public ViewResult Settings([FromRoute]string id)
    {
        var dto = RouteData.Values[
            MailingsUserSecuredServiceFilter.CheckedMailingKey] as MailingGroupDto;

        if (dto == null)
            throw new InvalidOperationException(
                "Dto from filter cannot be null.");

        var viewModel = new MailingRequestViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            MailType = dto.MailType,
        };
        return View(viewModel);
    }
    [HttpGet]
    public ViewResult ShowResponse([FromQuery] MailingResponseViewModel result)
    {
        string subject = "Mailing request error.";
        string to = "oleksii.havryk2004@gmail.com";
        string message = "Some unknown error with sending a request of applied " +
                         "new settings to mailing. ";
        string writeToEmailLink = $"https://mail.google.com/mail/?" +
                                  $"view=cm&" +
                                  $"fs=1&" +
                                  $"to={to}&" +
                                  $"su={subject}&" +
                                  $"body={message}&";

        ViewBag.WriteToEmailLink = writeToEmailLink;

        return View(result);
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Send([FromForm] MailingRequestViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(nameof(Settings));

        var request = new MailingRequestDto()
        {
            MailingId = viewModel.Id,
            SendType = viewModel.MailType
        };
        var response = await _senderResourceService.Send(request);

        var result = new MailingResponseViewModel()
        {
            Id = response.MailingId,
            IsSuccess = response.IsSuccess,
            Name = viewModel.Name
        };
        return RedirectToAction(nameof(ShowResponse), result);
    }
}