namespace Mailings.Web.Core.Services.Core;
/// <summary>
///     Class for providing authentication credentials to services which using
///     machine to machine authentication
/// </summary>
public sealed class AuthenticationOptions
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
    ///     List of scopes
    /// </summary>
    public List<string> Scopes { get; set; } = new List<string>();
}