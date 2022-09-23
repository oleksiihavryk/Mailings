namespace Mailings.Resources.Domain.Models;

public class EmailAddress
{
    public Guid Id { get; set; }
    public string AddressString { get; set; }
    public string UserId { get; set; }
}