using Mailings.Web.Shared.SystemConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Web.API.Controllers;
[Authorize(AuthorizationPolicyConstants.BetaTest)]
public sealed class MailingsController : Controller
{
}