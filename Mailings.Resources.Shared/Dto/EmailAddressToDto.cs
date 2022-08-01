namespace Mailings.Resources.Shared.Dto;

public class EmailAddressToDto
{
    public Guid Id { get; set; }
    public MailingGroupDto Group { get; set; }
    public EmailAddressDto Address { get; set; }
}