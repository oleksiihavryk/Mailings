namespace Mailings.Resources.Shared.Dto;

public class MailingGroupDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public IList<EmailAddressToDto> To { get; set; }
        = Enumerable.Empty<EmailAddressToDto>().ToList();
    public EmailAddressFromDto From { get; set; }
}