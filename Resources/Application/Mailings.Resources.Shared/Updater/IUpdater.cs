namespace Mailings.Resources.Shared.Updater;

public interface IUpdater
{
    void Update<T>(ref T obj1, T obj2);
    void Update<T>(ref T obj1, T obj2, params object[]? namesOfIgnoredProperties);
}