namespace Mailings.Authentication.Dto;
/// <summary>
///     DTO of user data.
/// </summary>
public sealed class UserDataDto
{
    /// <summary>
    ///     Username of user
    /// </summary>
    public string Username { get; set; } = string.Empty;
    /// <summary>
    ///     Password of user
    /// </summary>
    public string Password { get; set; } = string.Empty;
    /// <summary>
    ///     First name of user
    /// </summary>
    public string? FirstName { get; set; } = null;
    /// <summary>
    ///     Last name of user
    /// </summary>
    public string? LastName { get; set; } = null;
    /// <summary>
    ///     Email of user
    /// </summary>
    public string? Email { get; set; } = null;
}