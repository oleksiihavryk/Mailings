using Mailings.Resources.Shared.Dto;

namespace Mailings.Resources.API.Dto;

public class UnpreparedAddressToDto
{
    public string Address { get; set; }
    public Guid AddressId { get; set; }
    public Guid Id { get; set; }
}