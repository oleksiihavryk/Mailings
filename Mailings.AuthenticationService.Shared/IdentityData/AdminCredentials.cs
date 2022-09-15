namespace Mailings.AuthenticationService.Shared.IdentityData;
/// <summary>
///     Default admin account credentials in the system
/// </summary>
public sealed class AdminCredentials
{
    /// <summary> 
    ///     Login of admin account
    /// </summary>
    public string Login { get; set; } = string.Empty;
    /// <summary>
    ///     Password of admin account
    /// </summary>
    public string Password { get; set; } = string.Empty;
}