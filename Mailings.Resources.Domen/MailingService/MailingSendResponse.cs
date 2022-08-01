using Mailings.Resources.Domen.Models;

namespace Mailings.Resources.Domen.MailingService;

public class MailingSendResponse
{
    public bool IsSucceded { get; set; }
    public MailingGroup Group { get; set; }

    public HistoryNoteMailingGroup GetHistoryNote()
        => new HistoryNoteMailingGroup(Group, IsSucceded) 
            { When = DateTime.Now.ToUniversalTime() };
}