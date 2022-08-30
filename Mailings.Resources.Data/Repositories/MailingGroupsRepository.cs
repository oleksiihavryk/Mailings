using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Mailings.Resources.Shared.Updater;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class MailingGroupsRepository : IMailingGroupsRepository
{
    protected readonly CommonResourcesDbContext _dbContext;
    protected readonly IUpdater _updater;

    public MailingGroupsRepository(
        CommonResourcesDbContext dbContext,
        IUpdater updater)
    {
        _dbContext = dbContext;
        _updater = updater;
    }
    public virtual IEnumerable<MailingGroup> GetAll()
    {
        var entities = _dbContext.MailingGroups
            .Include(m => m.From)
            .Include(m => m.Mail)
            .Include(m => m.To)
                .ThenInclude(t => t.Address)
            .ToArray();

        return entities;
    }
    public virtual async Task<MailingGroup> GetByIdAsync(Guid key)
    {
        var entity = await _dbContext.MailingGroups
            .Include(g => g.From)
            .Include(g => g.Mail)
            .Include(g => g.To)
                .ThenInclude(t => t.Address)
            .FirstOrDefaultAsync(g => g.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(MailingGroup),
            dbContext: _dbContext);
    }
    public virtual async Task<MailingGroup> SaveAsync(MailingGroup entity)
    {
        await _dbContext.MailingGroups.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task<MailingGroup> UpdateAsync(MailingGroup entity)
    {
        var dbEntity = await GetByIdAsync(entity.Id);

        _updater.Update(
            obj1: ref dbEntity,
            obj2: entity,
            namesOfIgnoredProperties: nameof(MailingGroup.Id));

        await _dbContext.SaveChangesAsync();

        return dbEntity;
    }
    public virtual async Task DeleteByIdAsync(Guid key)
    {
        var entity = await GetByIdAsync(key);

        _dbContext.MailingGroups.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}