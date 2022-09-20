using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Authentication;
using IdentityModel;
using IdentityModel.Client;
using Mailings.Web.Core.Services.Interfaces;
using Mailings.Web.Shared.StaticData;

namespace Mailings.Web.Core.Services.Core;
/// <summary>
///     Implementation of authentication service request mode
/// </summary>
public class AuthenticationRequestMode : IAuthServiceRequestMode
{
    /// <summary>
    ///     Http client 
    /// </summary>
    protected readonly HttpClient _client;

    public AuthenticationRequestMode(
        IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
    }
    
    /// <summary>
    ///     Mode implementation of authentication service request
    /// </summary>
    /// <param name="request">
    ///     Service request
    /// </param>
    /// <param name="serviceUri">
    ///     Uri of service 
    /// </param>
    /// <param name="options">
    ///     Authentication options action
    /// </param>
    /// <returns>
    ///     Http request message object
    /// </returns>
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

    /// <summary>
    ///     Implementation of template method pattern
    ///     Configure request message uri and other common things...
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     Configuring request message
    /// </param>
    /// <param name="serviceUri">
    ///     URI of service
    /// </param>
    /// <param name="request">
    ///     Service request
    /// </param>
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
    /// <summary>
    ///     Implementation of template method pattern
    ///     Configure request message header
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     Configuring request message
    /// </param>
    /// <param name="request">
    ///     Service request
    /// </param>
    protected virtual void ConfigureHead(
        HttpRequestMessage httpRequestMessage,
        ServiceRequest request)
    {
        var headers = httpRequestMessage.Headers;

        headers.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpRequestMessage.Method = request.Method;
    }
    /// <summary>
    ///     Implementation of template method pattern
    ///     Configure request message body
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     Configuring request message
    /// </param>
    /// <param name="request">
    ///     Service request
    /// </param>
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
    /// <summary>
    ///     Implementation of template method pattern
    ///     Configure request message header
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     Configuring request message
    /// </param>
    /// <param name="authOptions">
    ///     Authentication options object
    /// </param>
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

    /// <summary>
    ///     Get authentication options object from options action
    /// </summary>
    /// <param name="options">
    ///     Authentication action options
    /// </param>
    /// <returns>
    ///     Authentication options object
    /// </returns>
    private AuthenticationOptions GetOptions(
        Action<AuthenticationOptions> options)
    {
        var authOptions = new AuthenticationOptions();
        options(authOptions);

        return authOptions;
    }
}