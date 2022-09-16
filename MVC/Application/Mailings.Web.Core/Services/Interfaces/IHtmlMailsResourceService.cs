using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services.Interfaces;
public interface IHtmlMailsResourceService
{
    Task<IEnumerable<MailDto>> GetMails();
    Task<IEnumerable<MailDto>> GetMailsByUserId(string userId);
    Task<MailDto> GetById(string id);
    Task<MailDto> Save(MailDto mailDto);
    Task<MailDto> Update(MailDto mailDto);
    Task Delete(string id);
}