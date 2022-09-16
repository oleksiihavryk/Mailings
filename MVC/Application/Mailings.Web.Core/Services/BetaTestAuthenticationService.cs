using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services;
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