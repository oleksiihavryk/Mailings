namespace Mailings.Authentication.Data.DbInitializer;
/// <summary>
///     Database initializer service
/// </summary>
public interface IDbInitializer
{
    /// <summary>
    ///     Initializing of database
    /// </summary>
    /// <returns>
    ///     Task of async operation by initializing database
    /// </returns>
    Task InitializeAsync();
}