using AutoMapper;
using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domen.MailingService;
using Mailings.Resources.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mailings.Resources.Data.Repositories;

public class HistoryNotesRepository : IHistoryNotesRepository
{
    private readonly CommonResourcesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<HistoryNotesRepository> _logger;

    public HistoryNotesRepository(
        CommonResourcesDbContext dbContext,
        IMapper mapper,
        ILogger<HistoryNotesRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<HistoryNoteMailingGroupDto> GetAll()
    {
        _logger.LogInformation("Trying to receive history note data " +
                               "from database.");
        try
        {
            var fromDb = _dbContext.MailingHistory.ToArray();
            var result = 
                _mapper.Map<HistoryNoteMailingGroupDto[]>(fromDb);

            _logger.LogInformation("History notes received successfully " +
                                   "from database");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get " +
                             $"all history notes data from database. " +
                             $"Error type: {ex.GetType()}, message {ex.Message}");
            throw;
        }
    }
    public async Task<HistoryNoteMailingGroupDto> GetByKeyAsync(Guid key)
    {
        _logger.LogInformation("Trying to receive history note from database " +
                               "by single key");
        try
        {
            var fromDbObject = await _dbContext.MailingHistory
                .FirstOrDefaultAsync(h => h.Id == key);

            if (fromDbObject == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(HistoryNoteMailingGroup),
                    dbContext: _dbContext);

            var result = _mapper.Map<HistoryNoteMailingGroupDto>(fromDbObject);

            _logger.LogInformation("History note from database by current key is " +
                                   "successfully received");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get value of history " +
                             $"note by key from database. Error type: {ex.GetType()}, " +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task<HistoryNoteMailingGroupDto> SaveIntoDbAsync(
        HistoryNoteMailingGroupDto entity)
    {
        _logger.LogInformation("Trying to save history note in database.");
        try
        {
            var historyNote = _mapper.Map<HistoryNoteMailingGroup>(entity);

            await _dbContext.MailingHistory
                .AddAsync(historyNote);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<HistoryNoteMailingGroupDto>(historyNote);

            _logger.LogInformation("Saving history note to database is " +
                                   "successfully completed.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to add or update " +
                             $"history note in database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        _logger.LogInformation("Trying to delete history note from database by key");
        try
        {
            var dbEntity = await _dbContext.MailingHistory
                .FirstOrDefaultAsync(h => h.Id == key);

            if (dbEntity == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(HistoryNoteMailingGroup),
                    dbContext: _dbContext);

            _dbContext.MailingHistory.Remove(dbEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleting history note from database is " +
                                   "successfully completed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to delete history note " +
                             $"from database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
}