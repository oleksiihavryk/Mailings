using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;

public interface IAccountAuthenticationService
{
    Task ChangeUserData(UserDataDto userData);
}