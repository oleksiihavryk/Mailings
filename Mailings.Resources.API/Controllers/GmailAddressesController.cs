using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers;

[ApiController]
[Route("api/gmail-addresses")]
[Authorize]
public class GmailAddressesController : ControllerBase
{
}