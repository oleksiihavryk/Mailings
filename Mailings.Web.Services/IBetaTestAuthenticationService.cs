using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public interface IBetaTestAuthenticationService
{
    Task<UserDataDto> GenerateAccount();
}