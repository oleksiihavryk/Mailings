using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class HistoryNotesRepository : IHistoryNotesRepository
{
    protected readonly CommonResourcesDbContext _dbContext;

    public HistoryNotesRepository(CommonResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual IEnumerable<HistoryNoteMailingGroup> GetAll()
    {
        var mailingHistory = _dbContext.MailingHistory.ToArray();

        return mailingHistory;
    }
    public virtual async Task<HistoryNoteMailingGroup> GetByKeyAsync(Guid key)
    {
        var mailingHistory = await _dbContext.MailingHistory
            .FirstOrDefaultAsync(h => h.Id == key);

        return mailingHistory ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(HistoryNoteMailingGroup),
            dbContext: _dbContext);
    }
    public virtual async Task<HistoryNoteMailingGroup> SaveIntoDbAsync(HistoryNoteMailingGroup entity)
    {
        await _dbContext.MailingHistory.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task DeleteFromDbByKey(Guid key)
    {
        var entity = await GetByKeyAsync(key);
        
        _dbContext.MailingHistory.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}