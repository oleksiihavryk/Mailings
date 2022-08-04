using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domain.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Repositories;

public class UserEmailRepository : IUserEmailsRepository
{
    private readonly CommonResourcesDbContext _dbContext;

    public UserEmailRepository(CommonResourcesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<UserMails> GetAll()
    {
        var entities = _dbContext.UserMails.ToArray();

        return entities;
    }
    public async Task<UserMails> GetByKeyAsync(Guid key)
    {
        var entity = await _dbContext.UserMails
            .FirstOrDefaultAsync(um => um.Id == key);

        return entity ?? throw new ObjectNotFoundInDatabaseException(
            typeOfObject: typeof(UserMails),
            dbContext: _dbContext);
    }
    public async Task<UserMails> SaveIntoDbAsync(UserMails entity)
    {
        await _dbContext.UserMails.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        var entity = await GetByKeyAsync(key);
    }
}