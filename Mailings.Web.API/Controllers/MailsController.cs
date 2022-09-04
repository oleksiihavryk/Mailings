using System.Collections.Immutable;
using System.Security.Claims;
using Mailings.Web.API.ViewModels;
using Mailings.Web.Services;
using Mailings.Web.Shared.Comparers;
using Mailings.Web.Shared.Dto;
using Mailings.Web.Shared.SystemConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class MailsController : Controller
{
    public const int StartPageIndex = 1;

    private readonly IHtmlMailsResourceService _htmlMailsService;
    private readonly ITextMailsResourceService _textMailsService;

    public MailsController(
        IHtmlMailsResourceService htmlMailsService, 
        ITextMailsResourceService textMailsService)
    {
        _htmlMailsService = htmlMailsService;
        _textMailsService = textMailsService;
    }

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
    public ViewResult Create() => View(new MailViewModel());
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm][FromBody]MailViewModel viewModel)
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
    public async Task<IActionResult> Delete(
        [FromRoute]string id,
        [FromRoute]MailTypeViewModel type)
    {
        await (type switch
        {
            MailTypeViewModel.Text => _textMailsService.Delete(id),
            MailTypeViewModel.Html => _htmlMailsService.Delete(id),
            _ => throw new InvalidOperationException("Unknown type of mail")
        });

        return RedirectToAction(nameof(All));
    }
    public async Task<IActionResult> Change(
        [FromRoute] string id,
        [FromRoute]MailTypeViewModel type)
    {
        var dto = await (type switch
        {
            MailTypeViewModel.Text => _textMailsService.GetById(id),
            MailTypeViewModel.Html => _htmlMailsService.GetById(id),
            _ => throw new InvalidOperationException("Unknown type of mail")
        });

        var model = ConvertToViewModel(dto, type);

        string names = string
            .Join(", ", model.Attachments
                .Select(f => f.Name));
        ViewBag.AttachmentsNames = names.Length > 15 ? 
            names.Substring(0, 15) + "..." : 
            names;

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Change([FromForm][FromBody]MailViewModel viewModel)
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

    private long GetCountOfBytesInAttachment(List<IFormFile> attachments)
        => attachments.Sum(a => a.Length);
    private MailViewModel ConvertToViewModel(MailDto dto, MailTypeViewModel type)
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
    private async Task<MailDto> PrepareMailDtoAsync(MailViewModel viewModel)
    {
        var mailDto = new MailDto()
        {
            Content = viewModel.Content,
            Theme = viewModel.Theme,
            Id = viewModel.Id
        };

        var attachmentList = new List<AttachmentDto>();
        
        foreach (var a in viewModel.Attachments)
        {
            var attachment = new AttachmentDto()
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
    private async Task<ImmutableSortedDictionary<MailDto, MailTypeViewModel>> GetUserMails()
    {
        var userId = (User.Identity as ClaimsIdentity)?.Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        if (userId == null)
            throw new InvalidOperationException(
                "User is not logged in in system!");

        var htmlMails = await _htmlMailsService.GetMailsByUserId(userId);
        var textMails = await _textMailsService.GetMailsByUserId(userId);
        var markedMails = new Dictionary<MailDto, MailTypeViewModel>();
        
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