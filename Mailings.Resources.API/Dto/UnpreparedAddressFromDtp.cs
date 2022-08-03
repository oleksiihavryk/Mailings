using Mailings.Resources.Shared.Dto;

namespace Mailings.Resources.API.Dto;

public class UnpreparedAddressFromDto
{
    public Guid Id { get; set; }
    public Guid AddressId { get; set; }
    public string Address { get; set; }
}