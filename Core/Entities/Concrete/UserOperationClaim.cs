using Core.Entities.Abstract;

namespace Core.Entities.Concrete;

public class UserOperationClaim : BaseEntity, IEntity
{
    public string UserId { get; set; }
    public string OperationClaimId { get; set; }
}