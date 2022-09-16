using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services.Interfaces;

public interface IMailingGroupsResourceService
{
    Task<IEnumerable<MailingGroupDto>> GetGroups();
    Task<IEnumerable<MailingGroupDto>> GetGroupsByUserId(string userId);
    Task<MailingGroupDto> GetById(string id);
    Task<MailingGroupDto> Save(MailingGroupDto group);
    Task<MailingGroupDto> Update(MailingGroupDto group);
    Task Delete(string id);
}