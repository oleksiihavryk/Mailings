using System.Runtime.Serialization;

namespace Mailings.Web.Shared.Extensions; 
/// <summary>
///     Serialization extension
/// </summary>
public static class SerializationExtensions 
{
    /// <summary>
    ///     Return from serializing object and cast him to chosen type
    /// </summary>
    /// <typeparam name="T">
    ///     Type of casting object
    /// </typeparam>
    /// <param name="info">
    ///     Serialization info
    /// </param>
    /// <param name="name">
    ///     Object key
    /// </param>
    /// <returns>
    ///     Object of current type
    /// </returns>
    /// <exception cref="SerializationException">
    ///     Occurred when received object by entered key is null 
    /// </exception>
    public static T GetObject<T>(this SerializationInfo info, string name)
        where T : class => (info.GetValue(name, typeof(T)) as T) ??
                           throw new SerializationException(
                               $"Serialization error while serializing " +
                               $"{name} field");
}