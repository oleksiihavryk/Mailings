using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Service for requesting to text mails endpoint of resource service
/// </summary>
public interface ITextMailsResourceService
{
    /// <summary>
    ///     Get all mails in system
    /// </summary>
    /// <returns>
    ///     Task of async operation by getting all mails in system
    /// </returns>
    Task<IEnumerable<Mail>> GetMails();
    /// <summary>
    ///     Get all mails in system by current user
    /// </summary>
    /// <param name="userId">
    ///     Mails of user with current identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by getting all mails in system by current user
    /// </returns>
    Task<IEnumerable<Mail>> GetMailsByUserId(string userId);
    /// <summary>
    ///     Get mail by identifier
    /// </summary>
    /// <param name="id">
    ///     Mail identifier
    /// </param>
    /// <returns>
    ///     Task of async operation by getting one mail by current identifier
    /// </returns>
    Task<Mail> GetById(string id);
    /// <summary>
    ///     Save mail in system
    /// </summary>
    /// <param name="mailDto">
    ///     Mail which has been saved in system
    /// </param>
    /// <returns>
    ///     Task of async operation by saving mail in system
    /// </returns>
    Task<Mail> Save(Mail mailDto);
    /// <summary>
    ///     Update already exists in system mail
    /// </summary>
    /// <param name="mailDto">
    ///     Mail which has been updated in system
    /// </param>
    /// <returns>
    ///     Task of async operation by updating mail in system
    /// </returns>
    Task<Mail> Update(Mail mailDto);
    /// <summary>
    ///     Deleting exist mail in system by identifier
    /// </summary>
    /// <param name="id">
    ///     Identifier of mail that has been deleted
    /// </param>
    /// <returns>
    ///     Task of async operation by deleting mail from system
    /// </returns>
    Task Delete(string id);
}