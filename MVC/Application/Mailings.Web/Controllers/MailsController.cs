using System.Collections.Immutable;
using System.Security.Claims;
using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Mailings.Web.Filters;
using Mailings.Web.Shared.Comparers;
using Mailings.Web.Shared.SystemConstants;
using Mailings.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.Controllers;
/// <summary>
///     Mails entities controller
/// </summary>
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class MailsController : Controller
{
    /// <summary>
    ///     Pagination started page anchor 
    /// </summary>
    public const int StartPageIndex = 1;

    /// <summary>
    ///     Html mails service
    /// </summary>
    private readonly IHtmlMailsResourceService _htmlMailsService;
    /// <summary>
    ///     Text mails service
    /// </summary>
    private readonly ITextMailsResourceService _textMailsService;

    public MailsController(
        IHtmlMailsResourceService htmlMailsService, 
        ITextMailsResourceService textMailsService)
    {
        _htmlMailsService = htmlMailsService;
        _textMailsService = textMailsService;
    }

    /// <summary>
    ///     Get all mail entities by user in view
    /// </summary>
    /// <param name="page">
    ///     Index of current page
    /// </param>
    /// <returns>
    ///     View of all groups 
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> All([FromRoute]int? page = null)
    {
        page = page ?? StartPageIndex;

        if (page < StartPageIndex)
            return NotFound();

        var mails = await GetUserMails();
        var viewMails = mails
            .Skip(((int)page - StartPageIndex) * ViewConstants.MailsCapacityPerPage)
            .Take(ViewConstants.MailsCapacityPerPage)
            .Select(el => ConvertToViewModel(el.Key, el.Value));

        if (!viewMails.Any() && page != StartPageIndex)
            return NotFound();

        int totalPages = (int) Math.Round(
            ((double)mails.Count() / ViewConstants.MailsCapacityPerPage),
            mode: MidpointRounding.ToPositiveInfinity);
        ViewBag.Pagination = new PaginationViewModel()
        {
            PageIndex = (int)page,
            TotalPages = totalPages != 0 ? totalPages : 1
        };

        return View(viewMails);
    }
    /// <summary>
    ///     View for created a mail by form input
    /// </summary>
    /// <returns>
    ///     View with form for creating a mail
    /// </returns>
    [HttpGet]
    public ViewResult Create() => View(new MailViewModel());
    /// <summary>
    ///     Delete a mail with current type and identifier
    /// </summary>
    /// <param name="id">
    ///     Identifier of mail what was be deleted
    /// </param>
    /// <param name="type">
    ///     Type of mail what was be deleted
    /// </param>
    /// <returns>
    ///     Redirect to action with all mails
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when mail type is unsupported with this action
    /// </exception>
    [HttpGet]
    [ServiceFilter(typeof(MailsUserSecuredServiceFilter))]
    public async Task<IActionResult> Delete(
        [FromRoute] string id,
        [FromRoute] MailTypeViewModel type)
    {
        try
        {
            await (type switch
            {
                MailTypeViewModel.Text => _textMailsService.Delete(id),
                MailTypeViewModel.Html => _htmlMailsService.Delete(id),
                _ => throw new InvalidOperationException("Unknown type of mail")
            });
        }
        catch (ObjectNotFoundException)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(All));
    }
    /// <summary>
    ///     Changing of mail with chosen identifier and type with form
    /// </summary>
    /// <param name="id">
    ///     Identifier of mail what was be changed
    /// </param>
    /// <param name="type">
    ///     Type of mail what was be changed
    /// </param>
    /// <returns>
    ///     View with form to changing mail
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred if service filter is worked incorrect and not return into
    ///     route data find by identifier model
    /// </exception>
    [HttpGet]
    [ServiceFilter(typeof(MailsUserSecuredServiceFilter))]
    public ViewResult Change(
        [FromRoute] string id,
        [FromRoute] MailTypeViewModel type)
    {
        MailViewModel? model = null;
        
        var dto = RouteData
            .Values[
                MailsUserSecuredServiceFilter.CheckedMailKey] as Mail;

        if (dto == null)
            throw new InvalidOperationException(
                "Dto is cannot be null because service filter " +
                "is checked mail by existing in system and " + 
                "returned that into route data.");

        model = ConvertToViewModel(dto, type);

        string names = string
            .Join(", ", model.Attachments
                .Select(f => f.Name));
        ViewBag.AttachmentsNames = names.Length > 15 ?
            names.Substring(0, 15) + "..." :
            names;

        return View(model);
    }
    /// <summary>
    ///     Changing a mail from form input
    /// </summary>
    /// <param name="viewModel">
    ///     Form input view model
    /// </param>
    /// <returns>
    ///     Redirect to action with all mails
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when mail type in view model is unhandled with this action
    /// </exception>
    [ValidateAntiForgeryToken, HttpPost]
    public async Task<IActionResult> Change(
        [FromForm]MailViewModel viewModel)
    {
        if (GetCountOfBytesInAttachment(viewModel.Attachments) >= 1024 * 1024 * 25) //25 mb
        {
            ModelState.AddModelError(string.Empty,
                "Summary data of attachments is cannot be bigger than 25 megabytes. " +
                "Please send attachments to google drive disk and insert link in email.");
        }
        if (viewModel.Id == Guid.Empty)
        {
            ModelState.AddModelError(string.Empty,
                "For updating entity id must be included into form.");
        }

        if (!ModelState.IsValid)
            return View(viewModel);

        var mailDto = await PrepareMailDtoAsync(viewModel);

        if (viewModel.Type == MailTypeViewModel.Text)
            await _textMailsService.Update(mailDto);
        else if (viewModel.Type == MailTypeViewModel.Html)
            await _htmlMailsService.Update(mailDto);
        else throw new InvalidOperationException("Unknown mail type");

        return RedirectToAction(nameof(All));
    }
    /// <summary>
    ///     Create a mail with input from form
    /// </summary>
    /// <param name="viewModel">
    ///     Form input view model
    /// </param>
    /// <returns>
    ///     Redirect to action with all mails
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when mail type in view model is unhandled with this action
    /// </exception>
    [ValidateAntiForgeryToken, HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] MailViewModel viewModel)
    {
        if (GetCountOfBytesInAttachment(viewModel.Attachments) >= 1024 * 1024 * 25) //25 mb
        {
            ModelState.AddModelError(string.Empty,
                "Summary data of attachments is cannot be bigger than 25 megabytes. " +
                "Please send attachments to google drive disk and insert link in email.");
        }

        if (!ModelState.IsValid)
            return View(viewModel);

        var mailDto = await PrepareMailDtoAsync(viewModel);

        if (viewModel.Type == MailTypeViewModel.Text)
            await _textMailsService.Save(mailDto);
        else if (viewModel.Type == MailTypeViewModel.Html)
            await _htmlMailsService.Save(mailDto);
        else throw new InvalidOperationException("Unknown mail type");

        return RedirectToAction(nameof(All));
    }

    /// <summary>
    ///     Get count of bytes in attachments
    /// </summary>
    /// <param name="attachments">
    ///     List of attachments from form
    /// </param>
    /// <returns>
    ///     Number of all bytes in attachments
    /// </returns>
    private long GetCountOfBytesInAttachment(List<IFormFile> attachments)
        => attachments.Sum(a => a.Length);
    /// <summary>
    ///     Convert mail from model object to view model
    /// </summary>
    /// <param name="dto">
    ///     Mail object model
    /// </param>
    /// <param name="type">
    ///     Type of mail
    /// </param>
    /// <returns>
    ///     View model of object
    /// </returns>
    private MailViewModel ConvertToViewModel(
        Mail dto,
        MailTypeViewModel type)
    {
        var model = new MailViewModel()
        {
            Id = dto.Id,
            Content = dto.Content,
            Theme = dto.Theme,
            Type = type
        };

        model.Attachments = dto.Attachments
            .Select(arg =>
            {
                using var ms = new MemoryStream(arg.BytesContent);
                IFormFile ff = new FormFile(
                    baseStream: ms,
                    baseStreamOffset: 0,
                    length: ms.Length,
                    name: arg.Name,
                    fileName: arg.Name)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = arg.ContentType
                };
                return ff;
            })
            .ToList();

        return model;
    }
    /// <summary>
    ///     Prepare mail with view model
    /// </summary>
    /// <param name="viewModel">
    ///     View model for preparing service model object
    /// </param>
    /// <returns>
    ///     Mail service object
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when user is not logged in system
    /// </exception>
    private async Task<Mail> PrepareMailDtoAsync(MailViewModel viewModel)
    {
        var mailDto = new Mail()
        {
            Content = viewModel.Content,
            Theme = viewModel.Theme,
            Id = viewModel.Id
        };

        var attachmentList = new List<Attachment>();
        
        foreach (var a in viewModel.Attachments)
        {
            var attachment = new Attachment()
            {
                ContentType = a.ContentType,
                Name = a.FileName
            };
            await using var attachmentStream = a.OpenReadStream();
            byte[] bytes = new byte[attachmentStream.Length];

            int readBytes = 0;

            do readBytes = await attachmentStream.ReadAsync(bytes);
            while (readBytes > 0);
            
            attachment.BytesContent = bytes;
            
            attachmentList.Add(attachment);
        }

        mailDto.Attachments = attachmentList;

        mailDto.UserId = (User.Identity as ClaimsIdentity)?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value ?? throw new InvalidOperationException(
                "User is not logged in in system");

        return mailDto;
    }
    /// <summary>
    ///     Get all user mails of current user
    /// </summary>
    /// <returns>
    ///     All mails of user
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when user is not logged in system
    /// </exception>
    private async Task<ImmutableSortedDictionary<Mail, MailTypeViewModel>> GetUserMails()
    {
        var userId = (User.Identity as ClaimsIdentity)?.Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        if (userId == null)
            throw new InvalidOperationException(
                "User is not logged in in system!");

        var htmlMails = await _htmlMailsService.GetMailsByUserId(userId);
        var textMails = await _textMailsService.GetMailsByUserId(userId);
        var markedMails = new Dictionary<Mail, MailTypeViewModel>();
        
        foreach (var htmlMail in htmlMails)
            markedMails[htmlMail] = MailTypeViewModel.Html;

        foreach (var textMail in textMails)
            markedMails[textMail] = MailTypeViewModel.Text;

        var mails = markedMails
            .ToImmutableSortedDictionary(
                new MailComparer(predicate: (x, y) => 
                    string.Compare(x.Theme, y.Theme, StringComparison.Ordinal)));

        return mails;
    }
}