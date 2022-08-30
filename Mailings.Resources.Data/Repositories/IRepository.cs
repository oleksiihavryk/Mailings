namespace Mailings.Resources.Data.Repositories;
public interface IRepository<TDtoEntity, TKey>
{
    IEnumerable<TDtoEntity> GetAll();
    Task<TDtoEntity> GetByIdAsync(TKey key);
    Task<TDtoEntity> SaveAsync(TDtoEntity entity);
    Task<TDtoEntity> UpdateAsync(TDtoEntity entity);
    Task DeleteByIdAsync(TKey key);
}