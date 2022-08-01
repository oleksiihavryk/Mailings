using AutoMapper;
using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domen.Models;
using Mailings.Resources.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mailings.Resources.Data.Repositories;
public class HtmlMailsRepository : IHtmlMailsRepository
{
    private readonly CommonResourcesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<HtmlMailsRepository> _logger;

    public HtmlMailsRepository(
        CommonResourcesDbContext dbContext,
        IMapper mapper,
        ILogger<HtmlMailsRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<HtmlMailDto> GetAll()
    {
        _logger.LogInformation("Trying to receive html mails data " +
                               "from database.");
        try
        {
            var fromDb = _dbContext.HtmlMails.ToArray();
            var result = _mapper.Map<HtmlMailDto[]>(fromDb);

            _logger.LogInformation("Html mails received successfully " +
                                   "from database");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get " +
                             $"all html mails data from database. " +
                             $"Error type: {ex.GetType()}, message {ex.Message}");
            throw;
        }
    }
    public async Task<HtmlMailDto> GetByKeyAsync(Guid key)
    {
        _logger.LogInformation("Trying to receive html mail from database " +
                               "by single key");
        try
        {
            var fromDbObject = await _dbContext.HtmlMails
                .FirstOrDefaultAsync(m => m.Id == key);

            if (fromDbObject == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(HtmlMail),
                    dbContext: _dbContext);

            var result = _mapper.Map<HtmlMailDto>(fromDbObject);

            _logger.LogInformation("Value of html mail by current key is " +
                                   "successfully received");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get value of html " +
                             $"mail by key from database. Error type: {ex.GetType()}, " +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task<HtmlMailDto> SaveIntoDbAsync(HtmlMailDto entity)
    {
        _logger.LogInformation("Trying to save html mail in database.");
        try
        {
            var htmlMail = _mapper.Map<HtmlMail>(entity);

            await _dbContext.HtmlMails
                .AddAsync(htmlMail);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<HtmlMailDto>(htmlMail);

            _logger.LogInformation("Saving html mail to database is " +
                                   "successfully completed.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to add or update " +
                             $"html mail in database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        _logger.LogInformation("Trying to delete html mail from database by key");
        try
        {
            var dbEntity = await _dbContext.HtmlMails
                .FirstOrDefaultAsync(t => t.Id == key);

            if (dbEntity == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(HtmlMail),
                    dbContext: _dbContext);

            _dbContext.HtmlMails.Remove(dbEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleting html mail from database is " +
                                   "successfully completed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to delete html mail " +
                             $"from database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
}