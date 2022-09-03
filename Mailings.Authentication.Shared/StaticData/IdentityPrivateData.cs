using Mailings.Authentication.Shared.Exceptions;

namespace Mailings.Authentication.Shared.StaticData;
public static class IdentityPrivateData
{
    public static IdentityClientData Resources => new IdentityClientData();
    public static IdentityClientData Authentication => new IdentityClientData();
    public static IdentityClientData WebUser => new IdentityClientData();
    //must to be initialized!
    public static string FullAccessScopeName { get; set; } = null!;
    public static string WriteDefaultScopeName { get; set; } = null!;
    public static string ReadDefaultScopeName { get; set; } = null!;
    public static string ReadSecuredScopeName { get; set; } = null!;
    public static string ResourcesApiResource { get; set; } = null!;
    

    public class IdentityClientData
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public List<string> Scopes { get; set; } 
    }

    public static void SetupClientData(
        Action<IdentityClientData> opt, 
        IdentityClient client)
    {
        var setupObj = client switch
        {
            IdentityClient.Resources => Resources,
            IdentityClient.Authentication => Authentication,
            IdentityClient.WebUser => WebUser,
            _ => throw new UnknownIdentityClientException()
        };

        opt(setupObj);
    }
}