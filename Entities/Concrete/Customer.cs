using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Customer : BaseEntity, IEntity
{
    public string CustomerId { get; set; }
    public string ContactName { get; set; }
    public string CompanyName { get; set; }
    public string City { get; set; }
}