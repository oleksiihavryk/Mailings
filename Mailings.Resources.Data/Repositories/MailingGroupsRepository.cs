using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class MailingGroupsRepository : IMailingGroupsRepository
{
    private readonly CommonResourcesDbContext _dbContext;

    public MailingGroupsRepository(CommonResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<MailingGroup> GetAll()
    {
        var entities = _dbContext.MailingGroups.ToArray();

        return entities;
    }
    public async Task<MailingGroup> GetByKeyAsync(Guid key)
    {
        var entity = await _dbContext.MailingGroups
            .FirstOrDefaultAsync(m => m.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(MailingGroup),
            dbContext: _dbContext);
    }
    public async Task<MailingGroup> SaveIntoDbAsync(MailingGroup entity)
    {
        await _dbContext.MailingGroups.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        var entity = await GetByKeyAsync(key);

        _dbContext.MailingGroups.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}