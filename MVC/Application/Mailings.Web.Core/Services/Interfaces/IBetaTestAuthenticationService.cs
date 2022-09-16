using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services.Interfaces;
public interface IBetaTestAuthenticationService
{
    Task<UserDataDto> GenerateAccount();
}