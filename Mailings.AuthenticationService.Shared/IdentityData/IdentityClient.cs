namespace Mailings.AuthenticationService.Shared.IdentityData;
/// <summary>
///     Identity client types in the system
///     (servers which have an access to this service)
/// </summary>
public enum IdentityClient
{
    Unknown,
    Resources,
    Authentication,
    MvcClient
}