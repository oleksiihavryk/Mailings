using Mailings.Web.Services.Core;
using Mailings.Web.Services.Exceptions;
using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public class BetaTestAuthenticationService : IBetaTestAuthenticationService
{
    public const string RoutePrefix = "/api/account/beta-test";

    protected readonly AuthenticationService _authService;

    public BetaTestAuthenticationService(AuthenticationService authService)
    {
        _authService = authService;
    }

    public virtual async Task<UserDataDto> GenerateAccount()
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix
        };

        //send request
        var result = await _authService
            .SendAndReceiveResponse<UserDataDto>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(AuthenticationService));
        
        throw new RequestToServiceIsFailedException(
            nameOfService:nameof(AuthenticationService));
    }
}