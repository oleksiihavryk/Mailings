using Mailings.Web.Shared.Exceptions;
using Newtonsoft.Json;
using ObjectCloneException = Mailings.Web.Core.Exceptions.ObjectCloneException;

namespace Mailings.Web.Core.Cloner;

public sealed class JsonDeepCloner : IDeepCloner
{
    public T DeepClone<T>(T obj)
    {
        var objString = JsonConvert.SerializeObject(obj); 
        var clonedObject = JsonConvert.DeserializeObject<T>(objString);

        return clonedObject ?? throw new ObjectCloneException(
            cloningObject: obj,
            deepCloner: this);
    }
}