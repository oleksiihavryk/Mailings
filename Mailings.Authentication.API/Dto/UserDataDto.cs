namespace Mailings.Authentication.API.Dto;
public sealed class UserDataDto
{
    public string Username { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}