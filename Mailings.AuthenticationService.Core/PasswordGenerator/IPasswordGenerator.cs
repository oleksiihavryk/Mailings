namespace Mailings.AuthenticationService.Core.PasswordGenerator;
/// <summary>
///     Password generator service
/// </summary>
public interface IPasswordGenerator
{
    /// <summary>
    ///     Default symbols count when generating password
    /// </summary>
    public const int SymbolsCount = 20;
    /// <summary>
    ///     Maximal count of symbols in password
    /// </summary>
    public const int MaxSymbolsCount = 64;

    /// <summary>
    ///     Generating password
    /// </summary>
    /// <param name="symbolsCount">
    ///     Count of symbols which password have
    /// </param>
    /// <returns>
    ///     Password string
    /// </returns>
    public string Generate(int? symbolsCount = null);
}