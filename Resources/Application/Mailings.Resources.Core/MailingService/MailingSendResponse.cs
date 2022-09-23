using Mailings.Resources.Domain.Models;

namespace Mailings.Resources.Core.MailingService;

public sealed class MailingSendResponse
{
    public bool IsSuccess { get; set; }
    public MailingGroup Group { get; set; }

    public HistoryNoteMailingGroup GetHistoryNote()
        => new HistoryNoteMailingGroup(Group, IsSuccess) 
            { When = DateTime.Now.ToUniversalTime() };
}