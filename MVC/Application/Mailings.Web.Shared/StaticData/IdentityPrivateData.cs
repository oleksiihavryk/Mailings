using Mailings.Web.Shared.Exceptions;

namespace Mailings.Web.Shared.StaticData;
public static class IdentityPrivateData
{
    public static IdentityClientData Resources { get; } = new IdentityClientData();
    public static IdentityClientData Authentication { get; } = new IdentityClientData();
    

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
            _ => throw new UnknownIdentityClientException()
        };

        opt(setupObj);
    }
}