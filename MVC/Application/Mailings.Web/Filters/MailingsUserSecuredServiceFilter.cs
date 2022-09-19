using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mailings.Web.Filters;
/// <summary>
///     Service for checking of user access to defined mailing groups by identifier
/// </summary>
public class MailingsUserSecuredServiceFilter : UserSecuredServiceFilter
{
    /// <summary>
    ///     Key of checked mailing group
    /// </summary>
    public const string CheckedMailingKey = "checked mail key";

    /// <summary>
    ///     Mailing groups service
    /// </summary>
    private readonly IMailingGroupsResourceService _groupsResourceService;

    public MailingsUserSecuredServiceFilter(
        IMailingGroupsResourceService groupsResourceService)
    {
        _groupsResourceService = groupsResourceService;
    }

    /// <summary>
    ///     Checking id of mailing group on access to current user
    /// </summary>
    /// <param name="context">
    ///     Context of user request
    /// </param>
    /// <returns>
    ///     Task of async operation by checking identifier of mailing group for user
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Occurred when filter was been applied unsuccessfully to action
    /// </exception>
    public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var id = GetUserId(context);
        var groupId = context.RouteData.Values["id"] as string;

        if (id == null || groupId == null)
            throw new InvalidOperationException(
                "Incorrect applying of filter. Filter must be applied " +
                "ONLY on actions which already include Authorize filter and parameters " +
                "contains id of object");

        MailingGroup? group = null;

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