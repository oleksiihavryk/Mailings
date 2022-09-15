using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Mailings.AuthenticationService.Shared.IdentityData;

namespace Mailings.AuthenticationService;
/// <summary>
///     Options data of Identity Server
/// </summary>
internal static class IdentityServerStaticData
{
    /// <summary>
    ///     Resources of Identity Server
    /// </summary>
    public static IEnumerable<IdentityResource> IdentityResources 
        => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResource(OidcConstants.StandardScopes.Profile, new []
            {
                JwtClaimTypes.FamilyName,
                JwtClaimTypes.GivenName,
                JwtClaimTypes.PreferredUserName,
                JwtClaimTypes.Role
            })
        };
    /// <summary>
    ///     Api Resources of Identity Server
    ///     (All API which used this server for authentication and authorization)
    /// </summary>
    public static IEnumerable<ApiResource> ApiResources
        => new List<ApiResource>()
        {
            new ApiResource(IdentityPrivateData.ResourcesApiResource, 
                "Api resource to access resources on server side.")
            {
                Enabled = true,
                Scopes =
                {
                    IdentityPrivateData.PartialAccessApiResource,
                    IdentityPrivateData.FullAcessApiResource
                },
                ShowInDiscoveryDocument = true
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName,
                "Api resource to access authentication server.")
            {
                Enabled = true,
            }
        };
    /// <summary>
    ///     API Scopes of Identity Server
    /// </summary>
    public static IEnumerable<ApiScope> Scopes 
        => new List<ApiScope>()
        {
            new ApiScope(
                name: IdentityPrivateData.FullAcessApiResource,
                displayName: "Scope of resource server with full access to " +
                             "any endpoint in resource server side")
            {
                ShowInDiscoveryDocument = true,
            },
            new ApiScope(
                name: IdentityPrivateData.PartialAccessApiResource,
                displayName: "Scope of resource server with partial access to " +
                             "endpoints in resource server side")
            {
                ShowInDiscoveryDocument = true,
            },
            new ApiScope(
                name: IdentityServerConstants.LocalApi.ScopeName,
                displayName: "Scope of auth server with access to advanced api.")
        };
    /// <summary>
    ///     Service clients of Identity Server
    /// </summary>
    public static IEnumerable<Client> Clients
        => new List<Client>()
        {
            new Client()
            {
                ClientId = IdentityPrivateData.MvcClient.ClientId,
                ClientName = "MVC Web client application",

                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = IdentityPrivateData.MvcClient.Scopes,

                AlwaysIncludeUserClaimsInIdToken = true,

                ClientSecrets =
                {
                    new Secret(IdentityPrivateData.MvcClient.ClientSecret.ToSha256())
                },

                RequirePkce = true,

                RedirectUris =
                {
                    IdentityClients.MvcClient + "/signin-oidc",
                },
                PostLogoutRedirectUris =
                    { IdentityClients.MvcClient + "/signout-callback-oidc" },

                AllowAccessTokensViaBrowser = true,
            },
            new Client()
            {
                ClientId = IdentityPrivateData.Resources.ClientId,
                ClientSecrets =
                {
                    new Secret(IdentityPrivateData.Resources.ClientSecret.ToSha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes = IdentityPrivateData.Resources.Scopes,

                RedirectUris =
                {
                    "https://oauth.pstmn.io/v1/browser-callback",
                    IdentityClients.ResourceServer + "/oauth2-redirect.html",
                    IdentityClients.ResourceServer + "/signin-oidc",
                },
                PostLogoutRedirectUris =
                { IdentityClients.ResourceServer + "/signout-callback-oidc" },
            },
            new Client()
            {
                ClientId = IdentityPrivateData.Authentication.ClientId,
                ClientSecrets =
                {
                    new Secret(IdentityPrivateData.Authentication.ClientSecret.ToSha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes = IdentityPrivateData.Authentication.Scopes,

                RedirectUris =
                {
                    "https://localhost:7001/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:7001/signout-callback-oidc"
                },
            }
        };
}