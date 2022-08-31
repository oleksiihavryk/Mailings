using System.Security.Claims;
using Mailings.Web.API.ViewModels;
using Mailings.Web.Services;
using Mailings.Web.Shared.Dto;
using Mailings.Web.Shared.SystemConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class MailingsController : Controller
{
    public const int StartPageIndex = 1;

    private readonly IMailingGroupsResourceService _groupsResourceService;
    private readonly ITextMailsResourceService _textMailsResourceService;
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

    public async Task<IActionResult> All([FromRoute] int? page = null)
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
    public async Task<IActionResult> More(string id)
    {
        throw new NotImplementedException();
    }
    public async Task<IActionResult> Create()
    {
        var viewModel = new CreatedMailingsViewModel();
        var mails = await GetMailsByUser();

        return View((viewModel, mails));
    }
    public async Task<IActionResult> Create((CreatedMailingsViewModel, IEnumerable<MailViewModel>) mailingAndMails)
    {
        if (!mailingAndMails.Item1.To.Any())
        {
            ModelState.AddModelError("",
                "Mailing must contain some addresses of receivers.");
        }

        if (!ModelState.IsValid)
            return View(mailingAndMails);

        var group = mailingAndMails.Item1;

        var dto = new MailingGroupDto()
        {
            Id = group.Id,
            MailId = group.MailId,
            MailType = group.
        };
    }

    private async Task<IEnumerable<MailViewModel>> GetMailsByUser()
    {
        var userId = (User.Identity as ClaimsIdentity)?.Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        if (userId == null)
            throw new InvalidOperationException(
                "User is not logged in in system!");

        var htmlMails = (await _htmlMailsResourceService
            .GetMailsByUserId(userId))
            .Select(GetMailViewModel);
        var textMails = (await _textMailsResourceService
            .GetMailsByUserId(userId))
            .Select(GetMailViewModel);
        
        var mails = new List<MailViewModel>();

        mails.AddRange(htmlMails);
        mails.AddRange(textMails);

        return mails;
    }
    private async Task<MailingViewModel> ConvertToViewModelAsync(MailingGroupDto dto)
        => new MailingViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            To = dto.To,
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
    private async Task<IEnumerable<MailingGroupDto>> GetMailingsByUser()
    {
        var userId = (User.Identity as ClaimsIdentity)?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value ?? throw new InvalidOperationException(
                "User is not login in system");

        var groups = await _groupsResourceService
            .GetGroupsByUserId(userId);

        return groups;
    }
    private MailViewModel ConvertMailToViewModel(
        MailDto dto,
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
    private MailViewModel GetMailViewModel(MailDto el)
        => new MailViewModel()
        {
            Id = el.Id,
            Theme = el.Theme,
            Type = MailTypeViewModel.Html
        };
}