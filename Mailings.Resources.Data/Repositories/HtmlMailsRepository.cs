using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;
public class HtmlMailsRepository : IHtmlMailsRepository
{
    private readonly CommonResourcesDbContext _dbContext;

    public HtmlMailsRepository(CommonResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<HtmlMail> GetAll()
    {
        var entities = _dbContext.HtmlMails
            .Include(m => m.Attachments)
            .ToArray();

        return entities;
    }
    public async Task<HtmlMail> GetByKeyAsync(Guid key)
    {
        var entity = await _dbContext.HtmlMails
            .Include(m => m.Attachments)
            .FirstOrDefaultAsync(m => m.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(HtmlMail),
            dbContext: _dbContext);
    }
    public async Task<HtmlMail> SaveIntoDbAsync(HtmlMail entity)
    {
        await _dbContext.HtmlMails.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        var entity = await GetByKeyAsync(key);

        _dbContext.HtmlMails.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}