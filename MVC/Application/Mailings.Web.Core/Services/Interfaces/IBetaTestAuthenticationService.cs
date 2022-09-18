using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Core.Services.Interfaces;
public interface IBetaTestAuthenticationService
{
    Task<UserData> GenerateAccount();
}