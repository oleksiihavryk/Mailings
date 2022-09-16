namespace Mailings.Web.Core.Cloner;
public interface IDeepCloner
{
    T DeepClone<T>(T obj);
}