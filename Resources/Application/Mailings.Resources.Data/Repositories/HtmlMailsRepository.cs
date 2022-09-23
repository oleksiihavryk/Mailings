using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.Models;
using Mailings.Resources.Shared.Updater;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;
public class HtmlMailsRepository : IHtmlMailsRepository
{
    protected readonly CommonResourcesDbContext _dbContext;
    protected readonly IUpdater _updater;

    public HtmlMailsRepository(
        CommonResourcesDbContext dbContext, 
        IUpdater updater)
    {
        _dbContext = dbContext;
        _updater = updater;
    }

    public virtual IEnumerable<HtmlMail> GetAll()
    {
        var entities = _dbContext.HtmlMails
            .Include(m => m.Attachments)
            .ToArray();

        return entities;
    }
    public virtual async Task<HtmlMail> GetByIdAsync(Guid key)
    {
        var entity = await _dbContext.HtmlMails
            .Include(m => m.Attachments)
            .FirstOrDefaultAsync(m => m.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(HtmlMail),
            dbContext: _dbContext);
    }
    public virtual async Task<HtmlMail> SaveAsync(HtmlMail entity)
    {
        await _dbContext.HtmlMails.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task<HtmlMail> UpdateAsync(HtmlMail entity)
    {
        var dbEntity = await GetByIdAsync(entity.Id);

        _updater.Update(
            obj1: ref dbEntity,
            obj2: entity,
            namesOfIgnoredProperties: nameof(HtmlMail.Id));

        await _dbContext.SaveChangesAsync();

        return dbEntity;
    }
    public virtual async Task DeleteByIdAsync(Guid key)
    {
        var entity = await GetByIdAsync(key);

        _dbContext.HtmlMails.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}