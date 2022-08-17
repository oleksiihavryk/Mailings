using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public interface IHtmlMailsResourceService
{
    Task<IEnumerable<MailDto>> GetMails();
    Task<IEnumerable<MailDto>> GetMailsByUserId(string userId);
    Task<MailDto> GetById(string id);
    Task<MailDto> Save(MailDto mailDto);
    Task<MailDto> Update(MailDto mailDto);
    Task Delete(string id);
}