using AutoMapper;
using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domen.Models;
using Mailings.Resources.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mailings.Resources.Data.Repositories;
public class TextMailsRepository : ITextMailsRepository
{
    private readonly CommonResourcesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<TextMailsRepository> _logger;

    public TextMailsRepository(
        CommonResourcesDbContext dbContext,
        IMapper mapper,
        ILogger<TextMailsRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<TextMailDto> GetAll()
    {
        _logger.LogInformation("Trying to receive text mails data " +
                               "from database.");
        try
        {
            var fromDb = _dbContext.TextMails.ToArray();
            var result = _mapper.Map<TextMailDto[]>(fromDb);

            _logger.LogInformation("Text mails received successfully " +
                                   "from database");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get " +
                             $"all text mails data from database. " +
                             $"Error type: {ex.GetType()}, message {ex.Message}");
            throw;
        }
    }
    public async Task<TextMailDto> GetByKeyAsync(Guid key)
    {
        _logger.LogInformation("Trying to receive text mail from database " +
                               "by single key");
        try
        {
            var fromDbObject = await _dbContext.TextMails
                .FirstOrDefaultAsync(t => t.Id == key);

            if (fromDbObject == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(TextMail),
                    dbContext: _dbContext);

            var result = _mapper.Map<TextMailDto>(fromDbObject);

            _logger.LogInformation("Value of text mail by current key is " +
                                   "successfully received");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get value of text " +
                             $"mail by key from database. Error type: {ex.GetType()}, " +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task<TextMailDto> SaveIntoDbAsync(TextMailDto entity)
    {
        _logger.LogInformation("Trying to save text mail in database.");
        try
        {
            var textMail = _mapper.Map<TextMail>(entity);

            await _dbContext.TextMails
                .AddAsync(textMail);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<TextMailDto>(textMail);

            _logger.LogInformation("Saving text mail to database is " +
                                   "successfully completed.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to add or update " +
                             $"text mail in database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        _logger.LogInformation("Trying to delete text mail from database by key");
        try
        {
            var dbEntity = await _dbContext.TextMails
                .FirstOrDefaultAsync(t => t.Id == key);

            if (dbEntity == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(TextMail),
                    dbContext: _dbContext);

            _dbContext.TextMails.Remove(dbEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleting text mail from database is " +
                                   "successfully completed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to delete text mail " +
                             $"from database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
}