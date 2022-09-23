using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.Models;
using Mailings.Resources.Shared.Updater;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class TextMailsRepository : ITextMailsRepository
{
    protected readonly CommonResourcesDbContext _dbContext;
    protected readonly IUpdater _updater;

    public TextMailsRepository(
        CommonResourcesDbContext dbContext, 
        IUpdater updater)
    {
        _dbContext = dbContext;
        _updater = updater;
    }

    public virtual IEnumerable<TextMail> GetAll()
    {
        var entities = _dbContext.TextMails
            .Include(m => m.Attachments)
            .ToArray();

        return entities;
    }
    public virtual async Task<TextMail> GetByIdAsync(Guid key)
    {
        var entity = await _dbContext.TextMails
            .Include(m => m.Attachments)
            .FirstOrDefaultAsync(m => m.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(TextMail),
            dbContext: _dbContext);
    }
    public virtual async Task<TextMail> SaveAsync(TextMail entity)
    {
        await _dbContext.TextMails.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task<TextMail> UpdateAsync(TextMail entity)
    {
        var dbEntity = await GetByIdAsync(entity.Id);

        _updater.Update(
            obj1: ref dbEntity,
            obj2: entity,
            namesOfIgnoredProperties: nameof(TextMail.Id));

        await _dbContext.SaveChangesAsync();

        return dbEntity;
    }
    public virtual async Task DeleteByIdAsync(Guid key)
    {
        var entity = await GetByIdAsync(key);
        
        _dbContext.TextMails.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}