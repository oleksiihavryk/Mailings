using Mailings.Web.Core.Exceptions;
using Mailings.Web.Core.Services.Core;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services;
/// <summary>
///     Implementation of beta test endpoint of authentication server service
/// </summary>
public class BetaTestAuthenticationService : IBetaTestAuthenticationService
{
    /// <summary>
    ///     Endpoint route prefix
    /// </summary>
    public const string RoutePrefix = "/api/account/beta-test";

    /// <summary>
    ///     Authentication server service
    /// </summary>
    protected readonly AuthenticationService _authService;

    public BetaTestAuthenticationService(AuthenticationService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Generating a beta test account
    /// </summary>
    /// <returns>
    ///     Account data of generated beta-test account
    /// </returns>
    /// <exception cref="UnknownResponseBodyFromRequestToServiceException">
    ///     Occurred when response body is empty or format is unhandled by this service
    /// </exception>
    /// <exception cref="RequestToServiceIsFailedException">
    ///     Occurred when response from service is failed
    /// </exception>
    public virtual async Task<UserData> GenerateAccount()
    {
        //setup request
        var request = new ServiceRequest(HttpMethod.Post)
        {
            RoutePrefix = RoutePrefix
        };

        //send request
        var result = await _authService
            .SendAndReceiveResponse<UserData>(request);

        //return result
        if (result.IsSuccess)
            return result.Result ??
                   throw new UnknownResponseBodyFromRequestToServiceException(
                       nameOfService: nameof(AuthenticationService));
        
        throw new RequestToServiceIsFailedException(
            nameOfService:nameof(AuthenticationService));
    }
}