using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
public interface IHtmlMailsResourceService
{
    Task<IEnumerable<Mail>> GetMails();
    Task<IEnumerable<Mail>> GetMailsByUserId(string userId);
    Task<Mail> GetById(string id);
    Task<Mail> Save(Mail mailDto);
    Task<Mail> Update(Mail mailDto);
    Task Delete(string id);
}