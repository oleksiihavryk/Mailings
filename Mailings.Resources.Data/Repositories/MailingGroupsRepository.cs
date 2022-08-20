using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class MailingGroupsRepository : IMailingGroupsRepository
{
    protected readonly CommonResourcesDbContext _dbContext;

    public MailingGroupsRepository(CommonResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
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
    public virtual async Task<MailingGroup> GetByKeyAsync(Guid key)
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
    public virtual async Task<MailingGroup> SaveIntoDbAsync(MailingGroup entity)
    {
        await _dbContext.MailingGroups.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task DeleteFromDbByKey(Guid key)
    {
        var entity = await GetByKeyAsync(key);

        _dbContext.MailingGroups.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}