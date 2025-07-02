using DatabaseBroker.DataContext;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories;

public class GenericRepository<T, TId>(IhdaDataContext dbContext)
    where T : ModelBase<TId>
{
    public async Task<T> AddWithSaveChangesAsync(T entity)
    {
        entity.IsDelete = false;
        var addedEntityEntry = await dbContext
            .Set<T>()
            .AddAsync(entity);

        await this.SaveChangesAsync();

        return addedEntityEntry.Entity;
    }
    public async Task AddAsync(T entity)
    {
        entity.IsDelete = false;
        await dbContext
            .Set<T>()
            .AddAsync(entity);
    }
    public async Task<T> UpdateWithSaveChangesAsync(T entity)
    {
        var updatedEntityEntry = dbContext
            .Set<T>()
            .Update(entity);

        await this.SaveChangesAsync();

        return updatedEntityEntry.Entity;
    }
    public async Task UpdateAsync(T entity)
    {
        var updatedEntityEntry = dbContext
            .Set<T>()
            .Update(entity);
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
    }
    public async Task AddRangeWithSaveChangesAsync(params T[] entities)
    {
        await dbContext
            .Set<T>()
            .AddRangeAsync(entities);
        await this.SaveChangesAsync();
    }
    public async Task UpdateRangeWithChangesAsync(params T[] entities)
    {
        dbContext
            .Set<T>()
            .UpdateRange(entities);

        await this.SaveChangesAsync();
    }
    public async Task UpdateRangeAsync(params T[] entities)
    {
        dbContext
            .Set<T>()
            .UpdateRange(entities);
    }
    public async Task<T> RemoveWithSaveChangesAsync(T entity)
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
    public async Task RemoveAsync(T entity)
    {
        var existingEntity =
            await dbContext
                .Set<T>()
                .FindAsync(entity.Id);

        if (existingEntity == null) return;
        existingEntity.IsDelete = true;
    }
    public async Task RemoveRangeWithSaveChangesAsync(params T[] entity)
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
    }
    private async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
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
    public IQueryable<T> GetAllWithDetails(string[]? includes = null,bool deleted = false)
    {
        var entities = dbContext
            .Set<T>()
            .Where(e => e.IsDelete == deleted);

        return includes == null ? entities : includes.Aggregate(entities, (current, include) => current.Include(include));
    }
}