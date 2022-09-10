using Mailings.Web.Services;
using Mailings.Web.Services.Exceptions;
using Mailings.Web.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mailings.Web.API.Filters;
public class MailingsUserSecuredServiceFilter : UserSecuredServiceFilter
{
    public const string CheckedMailingKey = "checked mail key";

    private readonly IMailingGroupsResourceService _groupsResourceService;

    public MailingsUserSecuredServiceFilter(
        IMailingGroupsResourceService groupsResourceService)
    {
        _groupsResourceService = groupsResourceService;
    }

    public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var id = GetUserId(context);
        var groupId = context.RouteData.Values["id"] as string;

        if (id == null || groupId == null)
            throw new InvalidOperationException(
                "Incorrect applying of filter. Filter must be applied " +
                "ONLY on actions which already include Authorize filter and parameters " +
                "contains id of object");

        MailingGroupDto? group = null;

        try
        {
            group = (await _groupsResourceService.GetGroupsByUserId(id))
                .FirstOrDefault(c => c.Id == Guid.Parse(groupId));
        }
        catch (ObjectNotFoundException) { }

        if (group == null)
        {
            context.Result = new ViewResult()
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            return;
        }

        context.RouteData.Values[CheckedMailingKey] = group;
    }
}