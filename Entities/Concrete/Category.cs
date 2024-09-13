using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Category : BaseEntity, IEntity
{
    public string CategoryName { get; set; }
}