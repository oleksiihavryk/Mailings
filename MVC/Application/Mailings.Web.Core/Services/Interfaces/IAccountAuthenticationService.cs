using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;

public interface IAccountAuthenticationService
{
    Task ChangeUserData(UserData userData);
}