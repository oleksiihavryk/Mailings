using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class TextMailsRepository : ITextMailsRepository
{
    protected readonly CommonResourcesDbContext _dbContext;

    public TextMailsRepository(CommonResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual IEnumerable<TextMail> GetAll()
    {
        var entities = _dbContext.TextMails
            .Include(m => m.Attachments)
            .ToArray();

        return entities;
    }
    public virtual async Task<TextMail> GetByKeyAsync(Guid key)
    {
        var entity = await _dbContext.TextMails
            .Include(m => m.Attachments)
            .FirstOrDefaultAsync(m => m.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(TextMail),
            dbContext: _dbContext);
    }
    public virtual async Task<TextMail> SaveIntoDbAsync(TextMail entity)
    {
        await _dbContext.TextMails.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public virtual async Task DeleteFromDbByKey(Guid key)
    {
        var entity = await GetByKeyAsync(key);
        
        _dbContext.TextMails.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}