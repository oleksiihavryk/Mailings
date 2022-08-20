namespace Mailings.Web.Services.Core;

public class AuthenticationOptions
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public List<string> Scopes { get; set; } = new List<string>();
}