namespace Mailings.Web.Core.Services.Core;

public interface IAuthServiceRequestMode
{
    Task<HttpRequestMessage> SetupAuthRequestMessageAsync(
        ServiceRequest request,
        Uri serviceUri,
        Action<AuthenticationOptions> options);
}