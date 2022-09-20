using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Service for requesting to mailing sender endpoint of resource service
/// </summary>
public interface IMailingsSenderResourceService
{
    /// <summary>
    ///     Send request of configure new options of mailing group
    /// </summary>
    /// <param name="requestDto">
    ///     Mailing request
    /// </param>
    /// <returns>
    ///     Mailing response object
    /// </returns>
    Task<MailingResponse> Send(MailingRequest requestDto);
}