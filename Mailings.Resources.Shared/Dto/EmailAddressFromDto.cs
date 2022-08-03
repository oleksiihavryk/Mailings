namespace Mailings.Resources.Shared.Dto;

public class EmailAddressFromDto
{
    public Guid Id { get; set; }
    public MailingGroupDto Group { get; set; }
    public EmailAddressDto Address { get; set; }
    public string Name { get; set; }
    public bool IsConfirmed { get; set; }
}