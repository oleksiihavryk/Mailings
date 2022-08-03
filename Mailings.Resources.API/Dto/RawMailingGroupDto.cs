using System.Collections;

namespace Mailings.Resources.API.Dto;

public class RawMailingGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public UnpreparedAddressFromDto From { get; set; }
    public IEnumerable<RawAddressToDto> To { get; set; }
}