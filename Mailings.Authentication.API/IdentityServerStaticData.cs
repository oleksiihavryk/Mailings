using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Mailings.Authentication.Shared.StaticData;

namespace Mailings.Authentication.API;
internal static class IdentityServerStaticData
{
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
    public static IEnumerable<ApiResource> ApiResources
        => new List<ApiResource>()
        {
            new ApiResource(IdentityPrivateData.ResourcesApiResource, 
                "Api resource to access resources on server side.")
            {
                Enabled = true,
                Scopes =
                {
                    IdentityPrivateData.ReadSecuredScopeName,
                    IdentityPrivateData.WriteDefaultScopeName,
                    IdentityPrivateData.FullAccessScopeName
                },
                ShowInDiscoveryDocument = true
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName,
                "Api resource to access authentication server.")
            {
                Enabled = true,
            }
        };
    public static IEnumerable<ApiScope> Scopes 
        => new List<ApiScope>()
        {
            new ApiScope(
                name: IdentityPrivateData.ReadDefaultScopeName,
                displayName: "Scope of resource server for any users")
            {
                ShowInDiscoveryDocument = false,
            },
            new ApiScope(
                name: IdentityPrivateData.ReadSecuredScopeName,
                displayName: "Scope of resource server with extra permissions")
            {
                ShowInDiscoveryDocument = false,
            },
            new ApiScope(
                name: IdentityPrivateData.WriteDefaultScopeName,
                displayName: "Scope of resource server with rights to" +
                             " writing data on resource server")
            {
                ShowInDiscoveryDocument = false,
            },
            new ApiScope(
                name: IdentityPrivateData.FullAccessScopeName,
                displayName: "Scope of resource server with full access to " +
                             "any endpoint in resource server side")
            {
                ShowInDiscoveryDocument = true,
            },
            new ApiScope(
                name: IdentityServerConstants.LocalApi.ScopeName,
                displayName: "Scope of auth server with access to advanced api.")
        };

    public static IEnumerable<Client> Clients
        => new List<Client>()
        {
            new Client()
            {
                ClientId = IdentityPrivateData.WebUser.ClientId,
                ClientName = "MVC Web client application",

                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = IdentityPrivateData.WebUser.Scopes,

                AlwaysIncludeUserClaimsInIdToken = true,

                ClientSecrets =
                {
                    new Secret(IdentityPrivateData.WebUser.ClientSecret.ToSha256())
                },

                RequirePkce = true,

                RedirectUris =
                {
                    IdentityClients.ClientServer + "/signin-oidc",
                },
                PostLogoutRedirectUris =
                    { IdentityClients.ClientServer + "/signout-callback-oidc" },

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