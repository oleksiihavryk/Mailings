using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
public interface IMailingsSenderResourceService
{
    Task<MailingResponse> Send(MailingRequest requestDto);
}