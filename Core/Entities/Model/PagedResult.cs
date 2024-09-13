namespace Core.Entities.Model;

public class PagedResult<TEntity>
{
    public List<TEntity> Records { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
}