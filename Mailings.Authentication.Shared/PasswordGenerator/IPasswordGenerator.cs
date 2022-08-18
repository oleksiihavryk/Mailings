namespace Mailings.Authentication.Shared.PasswordGenerator;

public interface IPasswordGenerator
{
    public const int SymbolsCount = 20;
    public const int MaxSymbolsCount = 64;

    public string Generate(int? symbolsCount = null);
}