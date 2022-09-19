using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Mailings.Web.Filters;
using Mailings.Web.Shared.SystemConstants;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     Controller of sender and configuration mailing groups
/// </summary>
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class SenderController : Controller
{
    /// <summary>
    ///     Mailings sender service
    /// </summary>
    private readonly IMailingsSenderResourceService _senderResourceService;

    public SenderController(IMailingsSenderResourceService senderResourceService)
    {
        _senderResourceService = senderResourceService;
    }

    /// <summary>
    ///     View form for setting mailing group
    /// </summary>
    /// <param name="id">
    ///     Identifier of mailing group what was be configured
    /// </param>
    /// <returns>
    ///     View with form for configuration mailing group
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when service filter is working incorrect
    /// </exception>
    [HttpGet]
    [ServiceFilter(typeof(MailingsUserSecuredServiceFilter))]
    public ViewResult Settings([FromRoute]string id)
    {
        var dto = RouteData.Values[
            MailingsUserSecuredServiceFilter.CheckedMailingKey] as MailingGroup;

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
    /// <summary>
    ///     Show view response from mailing group configuration
    /// </summary>
    /// <param name="result">
    ///     Result of group configuration from service
    /// </param>
    /// <returns>
    ///     Mailing configuration response view
    /// </returns>
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
    /// <summary>
    ///     Configuration of mailing group by form input
    /// </summary>
    /// <param name="viewModel">
    ///     View model from form input
    /// </param>
    /// <returns>
    ///     Redirection to action for showing response of configurated
    /// </returns>
    [HttpPost, AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Send([FromForm] MailingRequestViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(nameof(Settings));

        var request = new MailingRequest()
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