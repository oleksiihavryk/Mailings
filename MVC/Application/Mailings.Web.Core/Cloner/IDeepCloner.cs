namespace Mailings.Web.Core.Cloner;
/// <summary>
///     Deep cloning service
/// </summary>
public interface IDeepCloner
{
    /// <summary>
    ///     Deep clone any type of object
    /// </summary>
    /// <typeparam name="T">
    ///     Method will work with any object types
    /// </typeparam>
    /// <param name="obj">
    ///     Object that will be cloned
    /// </param>
    /// <returns>
    ///     Clone object
    /// </returns>
    T DeepClone<T>(T obj);
}