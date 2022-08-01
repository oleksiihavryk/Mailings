using AutoMapper;
using Mailings.Resources.Data.DbContexts;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Domen.Models;
using Mailings.Resources.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mailings.Resources.Data.Repositories;

public class MailingGroupsRepository : IMailingGroupsRepository
{
    private readonly CommonResourcesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<MailingGroupsRepository> _logger;

    public MailingGroupsRepository(
        CommonResourcesDbContext dbContext,
        IMapper mapper,
        ILogger<MailingGroupsRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<MailingGroupDto> GetAll()
    {
        _logger.LogInformation("Trying to receive mailing groups data " +
                               "from database.");
        try
        {
            var fromDb = _dbContext.MailingGroups.ToArray();
            var result = _mapper.Map<MailingGroupDto[]>(fromDb);

            _logger.LogInformation("Mailing groups received successfully " +
                                   "from database");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get " +
                             $"all mailing groups data from database. " +
                             $"Error type: {ex.GetType()}, message {ex.Message}");
            throw;
        }
    }
    public async Task<MailingGroupDto> GetByKeyAsync(Guid key)
    {
        _logger.LogInformation("Trying to receive data from database by single key");
        try
        {
            var fromDbObject = await _dbContext.MailingGroups
                .FirstOrDefaultAsync(mg => mg.Id == key);

            if (fromDbObject == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(MailingGroup),
                    dbContext: _dbContext);

            var result = _mapper.Map<MailingGroupDto>(fromDbObject);

            _logger.LogInformation("Value of mailing group from current key is " +
                                   "successfully received");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error while trying to get value of object " +
                             $"by key from database. Error type: {ex.GetType()}," +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task<MailingGroupDto> SaveIntoDbAsync(MailingGroupDto entity)
    {
        _logger.LogInformation("Trying to save mailing group in database.");
        try
        {
            var mailingGroup = _mapper.Map<MailingGroup>(entity);

            await _dbContext.MailingGroups
                .AddAsync(mailingGroup);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<MailingGroupDto>(mailingGroup);

            _logger.LogInformation("Saving mailing group to database is " +
                                   "successfully completed.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to add or update " +
                             $"mailing group in database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
    public async Task DeleteFromDbByKey(Guid key)
    {
        _logger.LogInformation("Trying to delete mailing group from database by key");
        try
        {
            var dbEntity = await _dbContext.MailingGroups
                .FirstOrDefaultAsync(mg => mg.Id == key);

            if (dbEntity == null)
                throw new ObjectNotFoundInDatabaseException(
                    typeOfObject: typeof(MailingGroup),
                    dbContext: _dbContext);

            _dbContext.MailingGroups.Remove(dbEntity);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleting mailing group from database is " +
                                   "successfully completed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred error when trying to delete mailing group " +
                             $"from database. Error type: {ex.GetType()}" +
                             $"Message: {ex.Message}");
            throw;
        }
    }
}