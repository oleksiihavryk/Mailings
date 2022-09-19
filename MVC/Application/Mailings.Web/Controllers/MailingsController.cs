using System.Net.Mail;
using System.Security.Claims;
using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Mailings.Web.Filters;
using Mailings.Web.Shared.SystemConstants;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     Mailings entities controller
/// </summary>
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class MailingsController : Controller
{
    /// <summary>
    ///     Pagination started page anchor 
    /// </summary>
    public const int StartPageIndex = 1;

    /// <summary>
    ///     Groups service
    /// </summary>
    private readonly IMailingGroupsResourceService _groupsResourceService;
    /// <summary>
    ///     Text mails service
    /// </summary>
    private readonly ITextMailsResourceService _textMailsResourceService;
    /// <summary>
    ///     Html mails service
    /// </summary>
    private readonly IHtmlMailsResourceService _htmlMailsResourceService;

    public MailingsController(
        IMailingGroupsResourceService groupsResourceService, 
        ITextMailsResourceService textMailsResourceService, 
        IHtmlMailsResourceService htmlMailsResourceService)
    {
        _groupsResourceService = groupsResourceService;
        _textMailsResourceService = textMailsResourceService;
        _htmlMailsResourceService = htmlMailsResourceService;
    }
    /// <summary>
    ///     Get all group entities by user in view
    /// </summary>
    /// <param name="page">
    ///     Index of current page
    /// </param>
    /// <returns>
    ///     View of all groups 
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> All(
        [FromRoute] int? page = null)
    {
        page = page ?? StartPageIndex;

        if (page < StartPageIndex)
            return NotFound();

        var groups = await GetMailingsByUser();
        var viewMails = groups
            .Skip(((int)page - StartPageIndex) * ViewConstants.MailsCapacityPerPage)
            .Take(ViewConstants.MailsCapacityPerPage)
            .Select(dto => ConvertToViewModelAsync(dto).Result);

        if (!viewMails.Any() && page != StartPageIndex)
            return NotFound();

        int totalPages = (int)Math.Round(
            ((double)groups.Count() / ViewConstants.MailsCapacityPerPage),
            mode: MidpointRounding.ToPositiveInfinity);
        ViewBag.Pagination = new PaginationViewModel()
        {
            PageIndex = (int)page,
            TotalPages = totalPages != 0 ? totalPages : 1
        };

        return View(viewMails);
    }
    /// <summary>
    ///     View with chosen group options
    /// </summary>
    /// <param name="id">
    ///     Identifier of chosen group
    /// </param>
    /// <returns>
    ///     View of group options
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when service filter is working incorrect
    /// </exception>
    [HttpGet]
    [ServiceFilter(typeof(MailingsUserSecuredServiceFilter))]
    public async Task<ViewResult> More(
        [FromRoute] string id)
    {
        var dto = RouteData
            .Values[
                MailingsUserSecuredServiceFilter.CheckedMailingKey] as MailingGroup;

        if (dto == null)
            throw new InvalidOperationException(
                "Dto is cannot be null because service filter " +
                "is checked email group by existing and " + 
                "returned that into route data.");

        var viewModel = await ConvertToViewModelAsync(dto);

        return View(viewModel);
    }
    /// <summary>
    ///     Change some data in mailing group
    /// </summary>
    /// <param name="id">
    ///     Identifier of chosen mailing group
    /// </param>
    /// <returns>
    ///     Redirect to action with all mailing group
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when service filter is working incorrect
    /// </exception>
    [HttpGet]
    [ServiceFilter(typeof(MailingsUserSecuredServiceFilter))]
    public async Task<ViewResult> Change(
        [FromRoute]string id)
    {
        var dto = RouteData
            .Values[
                MailingsUserSecuredServiceFilter.CheckedMailingKey] as MailingGroup;

        if (dto == null)
            throw new InvalidOperationException(
                "Dto is cannot be null because service filter " +
                "is checked email group by existing and " + 
                "returned that into route data.");

        var viewModel = await ConvertToViewModelAsync(dto);

        var mails = await GetUserMailsAsync();
        ViewData["Mails"] = mails;

        return View(viewModel);
    }
    /// <summary>
    ///     Delete chosen mailing group from system
    /// </summary>
    /// <param name="id">
    ///     Identifier of mailing group what will be deleted
    /// </param>
    /// <returns>
    ///     Redirect ot action with all mailing groups
    /// </returns>
    [HttpGet]
    [ServiceFilter(typeof(MailingsUserSecuredServiceFilter))]
    public async Task<IActionResult> Delete(
        [FromRoute]string id)
    {
        try
        {
            await _groupsResourceService.Delete(id);
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(All));
    }
    /// <summary>
    ///     View for creating mailing group
    /// </summary>
    /// <returns>
    ///     View with form for creating mailing group
    /// </returns>
    [HttpGet]
    public async Task<ViewResult> Create()
    {
        var mails = await GetUserMailsAsync();
        ViewData["Mails"] = mails;

        return View(new MailingViewModel());
    }
    /// <summary>
    ///     Creating mailing group by form input
    /// </summary>
    /// <param name="viewModel">
    ///     View model of mailing group from form
    /// </param>
    /// <returns>
    ///     Redirect to action with all mailing groups
    /// </returns>
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] MailingViewModel viewModel)
    {
        var mailsTask = GetUserMailsAsync();

        if (viewModel.To.Any())
        {
            foreach (var m in viewModel.To)
            { 
                if (!MailAddress.TryCreate(m, out var parsedMail))
                    ModelState.AddModelError(string.Empty,
                        $"Input \"{m}\" is not valid for a email value.");
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty,
                $"Mailings is does not have any receivers. Please, add at " +
                $"least one to create mailings.");
        }

        if (!ModelState.IsValid)
        {
            ViewData["Mails"] = await mailsTask;
            return View(viewModel);
        }

        var userData = GetUserData();
        var mails = await mailsTask;
        var mail = mails
            .FirstOrDefault(m => m.Id == viewModel.Mail.Id);

        if (mail == null)
        {
            ModelState.AddModelError(string.Empty,
                errorMessage: "While mailing group is creating, mail of current " +
                              "mailing group is deleted. Try again with another mail.");
            return View(viewModel);
        }

        var dto = new MailingGroup()
        {
            MailId = Guid.Parse(mail.Id),
            MailType = mail.Type,
            Name = viewModel.Name,
            To = viewModel.To,
            UserId = userData.UserId,
            SenderPseudo = userData.Pseudo
        };

        await _groupsResourceService.Save(dto);

        return RedirectToAction(nameof(All));
    }
    /// <summary>
    ///     Changing created mailing group by form input
    /// </summary>
    /// <param name="viewModel">
    ///     View model of mailing group from form
    /// </param>
    /// <returns>
    ///     Redirect to action with all mailing groups
    /// </returns>
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Change(
        [FromForm] MailingViewModel viewModel)
    {
        var mailsTask = GetUserMailsAsync();

        if (viewModel.To.Any())
        {
            foreach (var m in viewModel.To)
            {
                if (!MailAddress.TryCreate(m, out var parsedMail))
                    ModelState.AddModelError(string.Empty,
                        $"Input \"{m}\" is not valid for a email value.");
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty,
                $"Mailings is does not have any receivers. Please, add at " +
                $"least one to create mailings.");
        }

        if (!ModelState.IsValid)
        {
            ViewData["Mails"] = await mailsTask;
            return View(viewModel);
        }

        var userData = GetUserData();
        var mails = await mailsTask;
        var mail = mails
            .FirstOrDefault(m => m.Id == viewModel.Mail.Id);

        if (mail == null)
        {
            ModelState.AddModelError(string.Empty,
                errorMessage: "While mailing group is creating, mail of current " +
                              "mailing group is deleted. Try again with another mail.");
            return View(viewModel);
        }

        var dto = new MailingGroup()
        {
            Id = viewModel.Id,
            MailId = Guid.Parse(mail.Id),
            MailType = mail.Type,
            Name = viewModel.Name,
            To = viewModel.To,
            UserId = userData.UserId,
            SenderPseudo = userData.Pseudo
        };

        await _groupsResourceService.Update(dto);

        return RedirectToAction(nameof(All));
    }

    /// <summary>
    ///     Get authenticated user data
    /// </summary>
    /// <returns>
    ///     UserData object with encapsulated user data
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred if user is not authenticate in system
    /// </exception>
    private UserData GetUserData()
    {
        var userClaims = (User.Identity as ClaimsIdentity)?
                         .Claims ??
                         throw new InvalidOperationException(
                             "User is not authenticated in system.");

        var userId = userClaims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value ?? throw new InvalidOperationException(
            "User is not authenticated in system.");
        var pseudo = string.Join(" ", userClaims
                                          .Where(c => c.Type is 
                                              ClaimTypes.GivenName or 
                                              ClaimTypes.Surname)
                                          .Select(c => c.Value) ??
                                      throw new InvalidOperationException(
                                          "User is not authenticated in system."));

        return new UserData()
        {
            UserId = userId,
            Pseudo = pseudo
        };
    }
    /// <summary>
    ///     Get from system all mails by current user
    /// </summary>
    /// <returns>
    ///     Mails list of user 
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred if user is not authenticate in system
    /// </exception>
    private async Task<List<MailingMailViewModel>> GetUserMailsAsync()
    {
        var userId = (User.Identity as ClaimsIdentity)?.Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        if (userId == null)
            throw new InvalidOperationException(
                "User is not logged in in system!");

        var htmlMails = await _htmlMailsResourceService
            .GetMailsByUserId(userId);
        var textMails = await _textMailsResourceService
            .GetMailsByUserId(userId);
        var mails = new List<MailingMailViewModel>();

        foreach (var htmlMail in htmlMails)
            mails.Add(ConvertMailToViewModel(
                dto: htmlMail, 
                type: MailTypeViewModel.Html));

        foreach (var textMail in textMails)
            mails.Add(ConvertMailToViewModel(
                dto: textMail, 
                type: MailTypeViewModel.Text));

        return mails;
    }
    /// <summary>
    ///     Convert from model object to view model
    /// </summary>
    /// <param name="dto">
    ///     Model object what has been converted
    /// </param>
    /// <returns>
    ///     View model of mailing group
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    private async Task<MailingViewModel> ConvertToViewModelAsync(MailingGroup dto)
        => new MailingViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            To = dto.To.ToList(),
            Mail = dto.MailType switch
            {
                "Html" => ConvertMailToViewModel(
                    dto: await _htmlMailsResourceService.GetById(dto.MailId.ToString()),
                    type: MailTypeViewModel.Html),
                "Text" => ConvertMailToViewModel(
                    dto: await _textMailsResourceService.GetById(dto.MailId.ToString()),
                    type: MailTypeViewModel.Text),
                _ => throw new InvalidOperationException("Unknown mail type")
            }
        };
    /// <summary>
    ///     Convert mail from object model to view model
    /// </summary>
    /// <param name="dto">
    ///     Object model what has been converted
    /// </param>
    /// <param name="type">
    ///     Type of mail
    /// </param>
    /// <returns>
    ///     View model of mail
    /// </returns>
    private MailingMailViewModel ConvertMailToViewModel(
        Mail dto,
        MailTypeViewModel type)
        => new MailingMailViewModel()
        {
            Id = dto.Id.ToString(),
            Theme = dto.Theme,
            Type = type.ToString()
        };
    /// <summary>
    ///     Get all mailing groups by user
    /// </summary>
    /// <returns>
    ///     Mailing groups enumerable
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when user is not logged in system
    /// </exception>
    private async Task<IEnumerable<MailingGroup>> GetMailingsByUser()
    {
        var userId = (User.Identity as ClaimsIdentity)?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value ?? throw new InvalidOperationException(
                "User is not login in system");

        var groups = await _groupsResourceService
            .GetGroupsByUserId(userId);

        return groups;
    }

    /// <summary>
    ///     Class of encapsulated user data
    /// </summary>
    private class UserData
    {
        /// <summary>
        ///     Identifier of user
        /// </summary>
        public string UserId { get; init; } = string.Empty;
        /// <summary>
        ///     Pseudo of user
        /// </summary>
        public string Pseudo { get; init; } = string.Empty;
    }
}