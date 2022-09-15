using System.Runtime.Serialization;

namespace Mailings.Authentication.Shared.Extensions; 
public static class SerializationExtensions 
{
    public static T GetObject<T>(this SerializationInfo info, string name)
        where T : class => (info.GetValue(name, typeof(T)) as T) ??
                           throw new SerializationException(
                               $"Serialization error while serializing " +
                               $"{name} field");
}