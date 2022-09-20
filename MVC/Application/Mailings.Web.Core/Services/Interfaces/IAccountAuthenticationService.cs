using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Service for requesting to account endpoint of authentication service 
/// </summary>
public interface IAccountAuthenticationService
{
    /// <summary>
    ///     Change profile user data for current user
    /// </summary>
    /// <param name="userData">
    ///     User data
    /// </param>
    /// <returns>
    ///     Task of async operation by changing current user profile data
    /// </returns>
    Task ChangeUserData(UserData userData);
}