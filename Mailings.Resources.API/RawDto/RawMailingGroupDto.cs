namespace Mailings.Resources.API.RawDto;

public class RawMailingGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public RawAddressFromDto From { get; set; }
    public IEnumerable<RawAddressToDto> To { get; set; }
}