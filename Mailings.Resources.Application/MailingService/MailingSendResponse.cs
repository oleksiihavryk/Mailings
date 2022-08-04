using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Application.MailingService;

public class MailingSendResponse
{
    public bool IsSucceded { get; set; }
    public MailingGroup Group { get; set; }

    public HistoryNoteMailingGroup GetHistoryNote()
        => new HistoryNoteMailingGroup(Group, IsSucceded) 
            { When = DateTime.Now.ToUniversalTime() };
}