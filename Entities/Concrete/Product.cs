using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Product : BaseEntity, IEntity
{
    public string CategoryId { get; set; }
    public string ProductName { get; set; }
    public int UnitsInStock { get; set; }
    public decimal UnitPrice { get; set; }
}