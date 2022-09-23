namespace Mailings.Web.Domain.ServicesModels;
/// <summary>
///     User data model
/// </summary>
public sealed class UserData
{
    /// <summary>
    ///     Username of user
    /// </summary>
    public string Username { get; set; } = string.Empty;
    /// <summary>
    ///     User password
    /// </summary>
    public string Password { get; set; } = string.Empty;
    /// <summary>
    ///     User email
    /// </summary>
    public string? Email { get; set; } = null;
    /// <summary>
    ///     User first name
    /// </summary>
    public string? FirstName { get; set; } = null;
    /// <summary>
    ///     User last name
    /// </summary>
    public string? LastName { get; set; } = null;
}