using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Mailings.Resources.Data.Exceptions;

[Serializable]
public sealed class ObjectNotFoundInDatabaseException : Exception
{
    private readonly Type _typeOfObject;
    private readonly DbContext _dbContext;

    public override string Message => base.Message ?? "Object not found in database." +
                                       $"Object type: {_typeOfObject}," +
                                       $" Database object: {_dbContext.ToString()}";
    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["object type"] = _typeOfObject,
        ["database"] = _dbContext
    };

    public ObjectNotFoundInDatabaseException(
        Type typeOfObject,
        DbContext dbContext,
        string? message = null)
        :this (typeOfObject, dbContext, message, null)
    {
    }
    public ObjectNotFoundInDatabaseException(
        Type typeOfObject, 
        DbContext dbContext,
        string? message,
        Exception? inner = null)
        : base(message, inner)
    {
        _typeOfObject = typeOfObject;
        _dbContext = dbContext;
    }
}