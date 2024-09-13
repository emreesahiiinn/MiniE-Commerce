using System.Linq.Expressions;
using Core.Entities.Abstract;
using Core.Entities.Model;

namespace Core.DataAccess;

public interface IEntityRepository<T>
    where T : class, IEntity, new()
{
    Task<PagedResult<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        int page = 1,
        int pageSize = 10,
        Expression<Func<T, object>> orderBy = null,
        bool orderDescending = false
    );

    Task<T> GetAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> orderBy = null,
        bool descending = false);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}