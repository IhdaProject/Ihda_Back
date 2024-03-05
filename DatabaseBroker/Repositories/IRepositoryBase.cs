namespace DatabaseBroker.Repositories;

public interface IRepositoryBase<T,in TId> : IQueryable<T>
{
    IQueryable<T> GetAllAsQueryable(bool asNoTracking = false,bool deleted = false);
    IQueryable<T> GetAllWithDetails(string[] includes = null,bool deleted = false);
    Task<T?> GetByIdAsync(TId id, bool asNoTracking = false,bool deleted = false);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(params T[] entities);
    Task<T> UpdateAsync(T entity);
    Task UpdateRangeAsync(params T[] entities);
    Task<T> RemoveAsync(T entity);
    Task RemoveRangeAsync(params T[] entity);
}