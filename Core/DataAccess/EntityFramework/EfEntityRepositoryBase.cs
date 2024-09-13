using System.Linq.Expressions;
using Core.Entities.Abstract;
using Core.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public async Task<PagedResult<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = null,
        int page = 1,
        int pageSize = 10,
        Expression<Func<TEntity, object>> orderBy = null,
        bool orderDescending = false
    )
    {
        await using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter != null) query = query.Where(filter);

        var totalRecords = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        if (orderBy != null) query = orderDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

        var records = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>
        {
            Records = records,
            Page = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, object>> orderBy = null, bool descending = false)
    {
        await using var context = new TContext();
        var query = context.Set<TEntity>().Where(filter);

        if (orderBy != null) query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

        return await query.FirstOrDefaultAsync();
    }


    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        await using var context = new TContext();
        return await context.Set<TEntity>().AnyAsync(filter);
    }

    public async Task AddAsync(TEntity entity)
    {
        await using var context = new TContext();
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Added;
        await context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        await using var context = new TContext();
        context.Set<TEntity>().AddRange(entities);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await using var context = new TContext();
        var updatedEntity = context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await using var context = new TContext();
        var deletedEntity = context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        await context.SaveChangesAsync();
    }
}