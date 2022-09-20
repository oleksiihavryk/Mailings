using Newtonsoft.Json;
using Mailings.Web.Core.Exceptions;

namespace Mailings.Web.Core.Cloner;
/// <summary>
///     Deep cloning service what work like a json serializer and deserializer
/// </summary>
public sealed class JsonDeepCloner : IDeepCloner
{
    /// <summary>
    ///     Implementation of deep cloning service interface.
    ///     Clone any objects.
    /// </summary>
    /// <typeparam name="T">
    ///     Method is can clone any object types and encapsulate this function in generic method
    /// </typeparam>
    /// <param name="obj">
    ///     Object what will be cloned
    /// </param>
    /// <returns>
    ///     Clone of parameter object
    /// </returns>
    /// <exception cref="ObjectCloneException">
    ///     Encapsulate any exception into cloning service and save inner error in yourself
    /// </exception>
    public T DeepClone<T>(T obj)
    {
        try
        {
            var objString = JsonConvert.SerializeObject(obj);
            var clonedObject = JsonConvert.DeserializeObject<T>(objString) ??
                               throw new InvalidOperationException(
                                   message: "Deserialized object must be not a null");

            return clonedObject;
        }
        catch (Exception ex)
        {
            throw new ObjectCloneException(
                cloningObject: obj,
                deepCloner: this,
                message: "Exception in deep cloning service. " +
                         "Look into inner exceptions to get more info about error...",
                inner: ex);
        }
    }
}