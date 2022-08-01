namespace Mailings.Resources.Shared.Dto;
public class HistoryNoteMailingGroupDto
{
    public Guid Id { get; set; }
    public MailingGroupDto Group { get; set; }
    public DateTime When { get; set; }
    public bool IsSucceded { get; set; }
}