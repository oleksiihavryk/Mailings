namespace Mailings.Resources.Domain.Models;
public class HistoryNoteMailingGroup
{
    public Guid Id { get; set; }
    public MailingGroup? Group { get; set; }
    public DateTime When { get; set; }
    public bool IsSucceded { get; set; }

    public HistoryNoteMailingGroup(bool isSucceded)
    {
        IsSucceded = isSucceded;
    }
    public HistoryNoteMailingGroup(MailingGroup group, bool isSucceded)
    {
        Group = group;
        IsSucceded = isSucceded;
    }
}
