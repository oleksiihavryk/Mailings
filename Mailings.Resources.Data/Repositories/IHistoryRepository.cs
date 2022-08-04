using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Data.Repositories;

public interface IHistoryNotesRepository : IRepository<HistoryNoteMailingGroup, Guid>
{
}