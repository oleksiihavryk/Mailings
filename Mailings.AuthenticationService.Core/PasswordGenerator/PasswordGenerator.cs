using System.Text;

namespace Mailings.AuthenticationService.Core.PasswordGenerator;
/// <summary>
///     Password generator default implementation
/// </summary>
public class PasswordGenerator : IPasswordGenerator
{
    /// <summary>
    ///     Implementation of symbols count
    ///     (Default symbols count in password (when symbolsCount parameter is null))
    /// </summary>
    public const int SymbolsCount = 20;
    /// <summary>
    ///     Implementation of maximal symbols count
    ///     (maximal symbols which may have password)
    /// </summary>
    public const int MaxSymbolsCount = 64;

    /// <summary>
    ///     Generator of password
    /// </summary>
    /// <param name="symbolsCount">
    ///     Count of symbols that password have in
    /// </param>
    /// <returns>
    ///     Password string
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     If parameter symbolsCount less or equal zero or bigger than MaxSymbolsCount
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     Impossible exception inside the system.
    ///     Invoke if number generator inside is broken
    /// </exception>
    public string Generate(int? symbolsCount = null)
    {
        int i;
        if (symbolsCount != null)
        {
            if (symbolsCount <= 0 || symbolsCount >= MaxSymbolsCount)
                throw new ArgumentException(
                    message: $"Argument {nameof(symbolsCount)} is less than 0 " +
                             $"or bigger than max value of symbols",
                    paramName: nameof(symbolsCount));

            i = symbolsCount.Value;
        }
        else
        {
            i = SymbolsCount;
        }

        var sb = new StringBuilder();
        Random random = new Random();

        while (i --> 0)
        {
            var digit = (char)random.Next(48, 57);
            var lowerCaseLetter = (char)random.Next(65, 90);
            var upperCaseLetter = (char)random.Next(97, 122);

            var symbol = random.Next(1, 4) switch
            {
                1 => digit,
                2 => lowerCaseLetter,
                3 => upperCaseLetter,
                _ => throw new InvalidOperationException("Impossible exception")
            };

            sb.Append(symbol);
        }

        return sb.ToString();
    }
}