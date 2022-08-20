using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public class AccountAuthenticationService : IAccountAuthenticationService
{
    public async Task<bool> ChangeUserData(UserDataDto userData)
    {
        throw new NotImplementedException();
    }
}
public interface IAccountAuthenticationService
{
    Task<bool> ChangeUserData(UserDataDto userData);
}