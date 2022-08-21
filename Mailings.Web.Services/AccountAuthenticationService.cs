using Mailings.Web.Services.Core;
using Mailings.Web.Services.Exceptions;
using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public class AccountAuthenticationService : IAccountAuthenticationService
{
    public const string RoutePrefix = "/api/account";

    protected AuthenticationService _authService;

    public AccountAuthenticationService(AuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task ChangeUserData(UserDataDto userData)
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Put)
        {
            RoutePrefix = RoutePrefix + "/change",
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