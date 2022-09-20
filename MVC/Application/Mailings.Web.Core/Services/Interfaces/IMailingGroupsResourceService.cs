using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Service for requesting to mailing groups endpoint of resource service
/// </summary>
public interface IMailingGroupsResourceService
{
    /// <summary>
    ///     Get all mailing groups in system
    /// </summary>
    /// <returns>
    ///     Task of async operation by getting all mailing groups in system
    /// </returns>
    Task<IEnumerable<MailingGroup>> GetGroups();
    /// <summary>
    ///     Get all mailing groups in system by current user
    /// </summary>
    /// <param name="userId">
    ///     Mailing groups of user with current identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by getting all mailing groups in system by current user
    /// </returns>
    Task<IEnumerable<MailingGroup>> GetGroupsByUserId(string userId);
    /// <summary>
    ///     Get mailing group by identifier
    /// </summary>
    /// <param name="id">
    ///     Mailing group identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by getting one mailing group by current identifier
    /// </returns>
    Task<MailingGroup> GetById(string id);
    /// <summary>
    ///     Save mailing group in system
    /// </summary>
    /// <param name="group">
    ///     Mailing group which has been saved in system
    /// </param>
    /// <returns>
    ///     Task of async operation by saving mailing group in system
    /// </returns>
    Task<MailingGroup> Save(MailingGroup group);
    /// <summary>
    ///     Update already exists in system mailing group
    /// </summary>
    /// <param name="group">
    ///     Mailing group which has been updated in system
    /// </param>
    /// <returns>
    ///     Task of async operation by updating mailing group in system
    /// </returns>
    Task<MailingGroup> Update(MailingGroup group);
    /// <summary>
    ///     Deleting exist mailing group in system by identifier
    /// </summary>
    /// <param name="id">
    ///     Identifier of mailing group that has been deleted
    /// </param>
    /// <returns>
    ///     Task of async operation by deleting mailing group from system
    /// </returns>
    Task Delete(string id);
}