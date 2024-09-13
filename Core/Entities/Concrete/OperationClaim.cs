using Core.Entities.Abstract;

namespace Core.Entities.Concrete;

public class OperationClaim : BaseEntity, IEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
}