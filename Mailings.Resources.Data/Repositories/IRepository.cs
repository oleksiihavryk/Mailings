namespace Mailings.Resources.Data.Repositories;
public interface IRepository<TDtoEntity, TKey>
{
    IEnumerable<TDtoEntity> GetAll();
    Task<TDtoEntity> GetByKeyAsync(TKey key);
    Task<TDtoEntity> SaveIntoDbAsync(TDtoEntity entity);
    Task DeleteFromDbByKey(TKey key);
}