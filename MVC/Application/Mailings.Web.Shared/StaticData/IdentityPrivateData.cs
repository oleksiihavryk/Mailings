using Mailings.Web.Shared.Exceptions;

namespace Mailings.Web.Shared.StaticData;
/// <summary>
///     Static identity data
/// </summary>
public static class IdentityPrivateData
{
    /// <summary>
    ///     Identity client resource server data
    /// </summary>
    public static IdentityClientData Resources { get; } = new IdentityClientData();
    /// <summary>
    ///     Identity client authentication server data
    /// </summary>
    public static IdentityClientData Authentication { get; } = new IdentityClientData();
    
    /// <summary>
    ///     Identity client credentials data
    /// </summary>
    public class IdentityClientData
    {
        /// <summary>
        ///     Client identifier
        /// </summary>
        public string ClientId { get; set; } = string.Empty;
        /// <summary>
        ///     Client secret
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;
        /// <summary>
        ///     Client scopes
        /// </summary>
        public List<string> Scopes { get; set; } 
    }

    /// <summary>
    ///     Configure credentials of chosen server
    /// </summary>
    /// <param name="opt">
    ///     Action of credentials configuration
    /// </param>
    /// <param name="client">
    ///     Server client type
    /// </param>
    /// <exception cref="UnknownIdentityClientException">
    ///     Occurred when identity client type is unhandled with this method
    /// </exception>
    public static void SetupClientData(
        Action<IdentityClientData> opt, 
        IdentityClient client)
    {
        var setupObj = client switch
        {
            IdentityClient.Resources => Resources,
            IdentityClient.Authentication => Authentication,
            _ => throw new UnknownIdentityClientException()
        };

        opt(setupObj);
    }
}