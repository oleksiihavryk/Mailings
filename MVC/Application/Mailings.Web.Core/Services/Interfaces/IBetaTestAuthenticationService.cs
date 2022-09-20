using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
/// <summary>
///     Service for requesting to beta test account endpoint of authentication service 
/// </summary>
public interface IBetaTestAuthenticationService
{
    /// <summary>
    ///     Generating beta test account 
    /// </summary>
    /// <returns>
    ///     Task of async operation by generating account
    /// </returns>
    Task<UserData> GenerateAccount();
}