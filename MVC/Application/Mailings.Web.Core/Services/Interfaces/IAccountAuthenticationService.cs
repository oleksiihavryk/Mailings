using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services.Interfaces;

public interface IAccountAuthenticationService
{
    Task ChangeUserData(UserDataDto userData);
}