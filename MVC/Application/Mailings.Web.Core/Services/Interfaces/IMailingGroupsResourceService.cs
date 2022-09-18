using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;

public interface IMailingGroupsResourceService
{
    Task<IEnumerable<MailingGroup>> GetGroups();
    Task<IEnumerable<MailingGroup>> GetGroupsByUserId(string userId);
    Task<MailingGroup> GetById(string id);
    Task<MailingGroup> Save(MailingGroup group);
    Task<MailingGroup> Update(MailingGroup group);
    Task Delete(string id);
}