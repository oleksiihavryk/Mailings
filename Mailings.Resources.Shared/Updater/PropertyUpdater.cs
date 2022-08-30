namespace Mailings.Resources.Shared.Updater;

public class PropertyUpdater : IUpdater
{
    public virtual void Update<T>(ref T obj1, T obj2) 
        => Update(
            ref obj1,
            obj2, 
            namesOfIgnoredProperties:null);
    public virtual void Update<T>(
        ref T obj1, 
        T obj2, 
        params object[]? namesOfIgnoredProperties)
    {
        var props = typeof(T).GetProperties();

        foreach (var p in props)
        {
            bool isIgnored = namesOfIgnoredProperties.Contains(p.Name);

            if (p.CanWrite && p.CanRead && !isIgnored)
            {
                var val = p.GetValue(obj2);
                p.SetValue(obj1, value: val);
            }
        }
    }
}