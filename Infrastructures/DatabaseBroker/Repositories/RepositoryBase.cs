using System.Collections;
using System.Linq.Expressions;
using Entity.Models;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories;

public class RepositoryBase<T, TId>(DbContext dbContext) : IRepositoryBase<T, TId>, IAsyncEnumerable<T>
    where T : ModelBase<TId>
{
    public async Task<T> AddAsync(T entity)
    {
        entity.IsDelete = false;
        var addedEntityEntry = await dbContext
            .Set<T>()
            .AddAsync(entity);

        await this.SaveChangesAsync();

        return addedEntityEntry.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updatedEntityEntry = dbContext
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
        await dbContext
            .Set<T>()
            .AddRangeAsync(entities);
        await this.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(params T[] entities)
    {
        dbContext
            .Set<T>()
            .UpdateRange(entities);

        await this.SaveChangesAsync();
    }

    public async Task<T> RemoveAsync(T entity)
    {
        var existingEntity =
            await dbContext
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
            var existingEntity = await dbContext.Set<T>().FindAsync(entityOne.Id);

            if (existingEntity != null)
            {
                existingEntity.IsDelete = true;
            }
        }

        await this.SaveChangesAsync();
    }

    private async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

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
            return dbContext
                .Set<T>()
                .Where(e => e.IsDelete == deleted)
                .AsNoTracking();

        return dbContext
            .Set<T>()
            .Where(e => e.IsDelete == deleted);
    }
    public IQueryable<T> GetAllWithDetails(string[] includes = null,bool deleted = false)
    {
        var entities = dbContext
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
        return dbContext.Set<T>().GetAsyncEnumerator(cancellationToken);
    }
}