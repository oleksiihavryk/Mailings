using System.Text;

namespace Mailings.Authentication.Shared.PasswordGenerator;
public class PasswordGenerator : IPasswordGenerator
{
    public const int SymbolsCount = 20;
    public const int MaxSymbolsCount = 64;

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