namespace Mailings.Web.Shared.Cloner;
public interface IDeepCloner
{
    T DeepClone<T>(T obj);
}