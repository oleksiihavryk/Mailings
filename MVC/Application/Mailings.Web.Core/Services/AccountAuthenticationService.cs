using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services;
/// <summary>
///     Implementation of account endpoint of authentication server service
/// </summary>
public class AccountAuthenticationService : IAccountAuthenticationService
{
    /// <summary>
    ///     Route prefix of endpoint
    /// </summary>
    public const string RoutePrefix = "/api/account";
    
    /// <summary>
    ///     Authentication server service
    /// </summary>
    protected AuthenticationService _authService;

    public AccountAuthenticationService(AuthenticationService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Changing profile user data 
    /// </summary>
    /// <param name="userData">
    ///     User data which has been changed for user
    /// </param>
    /// <returns>
    ///     Task of async operation by changing user data
    /// </returns>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task ChangeUserData(UserData userData)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Put)
        {
            RoutePrefix = RoutePrefix,
            BodyObject = userData,
        };

        //send request
        var result = await _authService
            .SendAndReceiveEmptyResponse(request);

        //return result
        if (result.IsSuccess)
            return;

        throw new RequestToServiceIsFailedException(
            nameOfService: nameof(AuthenticationService));
    }
}