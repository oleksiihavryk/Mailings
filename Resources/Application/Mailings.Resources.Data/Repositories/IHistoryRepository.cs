using Mailings.Resources.Domain.Models;

namespace Mailings.Resources.Data.Repositories;

public interface IHistoryNotesRepository : IRepository<HistoryNoteMailingGroup, Guid>
{
}