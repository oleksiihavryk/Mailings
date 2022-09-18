using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services;
public class AccountAuthenticationService : IAccountAuthenticationService
{
    public const string RoutePrefix = "/api/account";

    protected AuthenticationService _authService;

    public AccountAuthenticationService(AuthenticationService authService)
    {
        _authService = authService;
    }

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