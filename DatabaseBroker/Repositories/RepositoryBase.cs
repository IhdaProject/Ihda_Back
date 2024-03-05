using System.Collections;
using System.Linq.Expressions;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories;

public class RepositoryBase<T, TId> : IRepositoryBase<T, TId>, IQueryable<T>, IAsyncEnumerable<T>
    where T : ModelBase<TId>
{
    private protected readonly DbContext _dbContext;

    public RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.IsDelete = false;
        var addedEntityEntry = await this._dbContext
            .Set<T>()
            .AddAsync(entity);

        await this.SaveChangesAsync();

        return addedEntityEntry.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updatedEntityEntry = this
            ._dbContext
            .Set<T>()
            .Update(entity);

        await this.SaveChangesAsync();

        return updatedEntityEntry.Entity;
    }

    public async Task<T?> GetByIdAsync(TId id, bool asNoTracking = false,bool deleted = false)
    {
        return await this.GetAllAsQueryable(asNoTracking)
            .Where(e => e.IsDelete == deleted)
            .FirstOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task AddRangeAsync(params T[] entities)
    {
        await this._dbContext
            .Set<T>()
            .AddRangeAsync(entities);
        await this.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(params T[] entities)
    {
        this
            ._dbContext
            .Set<T>()
            .UpdateRange(entities);

        await this.SaveChangesAsync();
    }

    public async Task<T> RemoveAsync(T entity)
    {
        var existingEntity =
            await this
            ._dbContext
            .Set<T>()
            .FindAsync(entity.Id);

        if (existingEntity == null) return existingEntity;
        existingEntity.IsDelete = true;

        await this.SaveChangesAsync();

        return existingEntity;
    }

    public async Task RemoveRangeAsync(params T[] entity)
    {
        foreach (var entityOne in entity)
        {
            var existingEntity = await this._dbContext.Set<T>().FindAsync(entityOne.Id);

            if (existingEntity != null)
            {
                existingEntity.IsDelete = true;
            }
        }

        await this.SaveChangesAsync();
    }

    private async Task SaveChangesAsync() => await this._dbContext.SaveChangesAsync();

    public IEnumerator<T> GetEnumerator()
    {
        return this
            .GetAllAsQueryable()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Type ElementType
    {
        get => this.GetAllAsQueryable().ElementType;
    }

    public Expression Expression
    {
        get => this.GetAllAsQueryable().Expression;
    }

    public IQueryProvider Provider
    {
        get => this.GetAllAsQueryable().Provider;
    }

    public IQueryable<T> GetAllAsQueryable(bool asNoTracking = false,bool deleted = false)
    {
        if (asNoTracking)
            return this._dbContext
                .Set<T>()
                .Where(e => e.IsDelete == deleted)
                .AsNoTracking();

        return this._dbContext
            .Set<T>()
            .Where(e => e.IsDelete == deleted);
    }
    public IQueryable<T> GetAllWithDetails(string[] includes = null,bool deleted = false)
    {
        IQueryable<T> entities = this._dbContext
            .Set<T>()
            .Where(e => e.IsDelete == deleted);

        if (includes != null)
            foreach (var include in includes)
            {
                entities = entities.Include(include);
            }

        return entities;
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return this._dbContext.Set<T>().GetAsyncEnumerator(cancellationToken);
    }
}