using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Authentication;
using IdentityModel;
using IdentityModel.Client;
using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.Services.Core;

public class AuthenticationRequestMode : IAuthServiceRequestMode
{
    protected readonly HttpClient _client;

    public AuthenticationRequestMode(
        IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
    }

    public virtual async Task<HttpRequestMessage> SetupAuthRequestMessageAsync(
        ServiceRequest request, 
        Uri serviceUri,
        Action<AuthenticationOptions> options)
    {
        var hrm = new HttpRequestMessage();
        var authOptions = GetOptions(options);

        ConfigureCommon(httpRequestMessage: hrm, serviceUri, request);
        ConfigureHead(httpRequestMessage: hrm, request);
        ConfigureBody(httpRequestMessage: hrm, request);
        await ConfigureAuthentication(httpRequestMessage: hrm, authOptions);

        return hrm;
    }

    protected virtual void ConfigureCommon(
        HttpRequestMessage httpRequestMessage, 
        Uri serviceUri,
        ServiceRequest request)
    {
        var uri = serviceUri.AbsoluteUri.TrimEnd('/');

        if (request.RoutePrefix != null)
            uri += request.RoutePrefix;

        if (request.QueryField != null) 
            uri += "?" + request.QueryField;

        httpRequestMessage.RequestUri = new Uri(uri);
    }
    protected virtual void ConfigureHead(
        HttpRequestMessage httpRequestMessage,
        ServiceRequest request)
    {
        var headers = httpRequestMessage.Headers;

        headers.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpRequestMessage.Method = request.Method;
    }
    protected virtual void ConfigureBody(
        HttpRequestMessage httpRequestMessage,
        ServiceRequest request)
    {
        var obj = request.BodyObject;

        if (obj != null)
        {
            httpRequestMessage.Content = JsonContent.Create(
                inputValue: obj,
                mediaType: new MediaTypeHeaderValue("application/json"));
        }
    }
    protected virtual async Task ConfigureAuthentication(
        HttpRequestMessage httpRequestMessage,
        AuthenticationOptions authOptions)
    {
        var tokenResponse = await _client
            .RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest()
                {
                    Address = Servers.Authentication + "/connect/token",

                    ClientId = authOptions.ClientId,
                    ClientSecret = authOptions.ClientSecret,

                    GrantType = OidcConstants.GrantTypes.ClientCredentials,
                    Scope = string.Join(',', authOptions.Scopes)
                });

        if (tokenResponse.IsError)
            throw new AuthenticationException(
                "Error of authentication on auth server side");

        httpRequestMessage.SetBearerToken(tokenResponse.AccessToken);
    }

    private AuthenticationOptions GetOptions(
        Action<AuthenticationOptions> options)
    {
        var authOptions = new AuthenticationOptions();
        options(authOptions);

        return authOptions;
    }
}