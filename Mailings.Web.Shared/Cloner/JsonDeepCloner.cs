using Mailings.Web.Shared.Exceptions;
using Newtonsoft.Json;

namespace Mailings.Web.Shared.Cloner;

public class JsonDeepCloner : IDeepCloner
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