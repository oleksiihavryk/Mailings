using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Mailings.Resources.Shared.Updater;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class HistoryNotesRepository : IHistoryNotesRepository
{
    protected readonly CommonResourcesDbContext _dbContext;
    protected readonly IUpdater _updater;

    public HistoryNotesRepository(
        CommonResourcesDbContext dbContext,
        IUpdater updater)
    {
        _dbContext = dbContext;
        _updater = updater;
    }

    public virtual IEnumerable<HistoryNoteMailingGroup> GetAll()
    {
        var mailingHistory = _dbContext.MailingHistory.ToArray();

        return mailingHistory;
    }
    public virtual async Task<HistoryNoteMailingGroup> GetByIdAsync(Guid key)
    {
        var mailingHistory = await _dbContext.MailingHistory
            .FirstOrDefaultAsync(h => h.Id == key);

        return mailingHistory ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(HistoryNoteMailingGroup),
            dbContext: _dbContext);
    }
    public virtual async Task<HistoryNoteMailingGroup> SaveAsync(
        HistoryNoteMailingGroup entity)
    {
        await _dbContext.MailingHistory.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task<HistoryNoteMailingGroup> UpdateAsync(
        HistoryNoteMailingGroup entity)
    {
        var dbEntity = await GetByIdAsync(entity.Id);

        _updater.Update(
            obj1: ref dbEntity,
            obj2: entity,
            namesOfIgnoredProperties: nameof(HistoryNoteMailingGroup.Id));

        await _dbContext.SaveChangesAsync();

        return dbEntity;
    }

    public virtual async Task DeleteByIdAsync(Guid key)
    {
        var entity = await GetByIdAsync(key);
        
        _dbContext.MailingHistory.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}