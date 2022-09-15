using Mailings.AuthenticationService.Shared.Exceptions;

namespace Mailings.AuthenticationService.Shared.IdentityData;
/// <summary>
///     Static options of identity server
/// </summary>
public static class IdentityPrivateData
{
    /// <summary>
    ///     Resources server client static data
    /// </summary>
    public static IdentityClientData Resources { get; } = new IdentityClientData();
    /// <summary>
    ///     Authentication server client static data
    /// </summary>
    public static IdentityClientData Authentication { get; }  = new IdentityClientData();
    /// <summary>
    ///     MVC Client server static data
    /// </summary>
    public static IdentityClientData MvcClient { get; } = new IdentityClientData();

    //must to be initialized!
    /// <summary>
    ///     Scope of partial access to api resource
    /// </summary>
    public static string PartialAccessApiResource { get; set; } = null!;
    /// <summary>
    ///     Scope of full access to api resource
    /// </summary>
    public static string FullAcessApiResource { get; set; } = null!;
    /// <summary>
    ///     API resource of resources server
    /// </summary>
    public static string ResourcesApiResource { get; set; } = null!;

    /// <summary>
    ///     Inner class is denotes a identity client data
    /// </summary>
    public class IdentityClientData
    {
        /// <summary>
        ///     Client id of client
        /// </summary>
        public string ClientId { get; set; } = string.Empty;
        /// <summary>
        ///     Client secret of client
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;
        /// <summary>
        ///     Client scopes of client
        /// </summary>
        public List<string> Scopes { get; set; } = new List<string>();
    }

    /// <summary>
    ///     Helping to setup client data
    /// </summary>
    /// <param name="opt">
    ///     User options for chosen identity client
    /// </param>
    /// <param name="client">
    ///     Chosen setup client
    /// </param>
    /// <exception cref="UnknownIdentityClientException">
    ///     Occurred if chosen identity client is not handle in this method
    /// </exception>
    public static void SetupClientData(
        Action<IdentityClientData> opt, 
        IdentityClient client)
    {
        var setupObj = client switch
        {
            IdentityClient.Resources => Resources,
            IdentityClient.Authentication => Authentication,
            IdentityClient.WebUser => MvcClient,
            _ => throw new UnknownIdentityClientException()
        };

        opt(setupObj);
    }
}