using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mailings.Web.Filters;
public class MailsUserSecuredServiceFilter : UserSecuredServiceFilter
{
    public const string CheckedMailKey = "checked mail key";

    private readonly IHtmlMailsResourceService _htmlMailsResourceService;
    private readonly ITextMailsResourceService _textMailsResourceService;

    public MailsUserSecuredServiceFilter(
        IHtmlMailsResourceService htmlMailsResourceService, 
        ITextMailsResourceService textMailsResourceService)
    {
        _htmlMailsResourceService = htmlMailsResourceService;
        _textMailsResourceService = textMailsResourceService;
    }

    public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var id = GetUserId(context);
        var mailId = context.RouteData.Values["id"] as string;

        if (id == null || mailId == null)
            throw new InvalidOperationException(
                "Incorrect applying of filter. Filter must be applied " +
                "ONLY on actions which already include Authorize filter and parameters " +
                "contains id of object");

        List<Mail> mails = new List<Mail>();
        mails.AddRange(await _htmlMailsResourceService.GetMailsByUserId(id));
        mails.AddRange(await _textMailsResourceService.GetMailsByUserId(id));

        Mail? mail = null;

        try
        {
            mail = mails.FirstOrDefault(m => m.Id == Guid.Parse(mailId));
        }
        catch (ObjectNotFoundException) { }
        try
        {
            if (mail == null)
                mail = mails.FirstOrDefault(m => m.Id == Guid.Parse(mailId));
        }
        catch (ObjectNotFoundException) { }

        if (mail == null)
        {
            context.Result = new ViewResult()
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        context.RouteData.Values[CheckedMailKey] = mail;
    }
}