using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Order : BaseEntity, IEntity
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipCity { get; set; }
}