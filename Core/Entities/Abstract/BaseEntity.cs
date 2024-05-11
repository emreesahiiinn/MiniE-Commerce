using Core.Entities.Enums;

namespace Core.Entities.Abstract;

public class BaseEntity : IEntity
{
    public Guid Id { get; set; }
    public long CreatedDate { get; set; }
    public long? UpdatedDate { get; set; }
    public string? CreatedUserId { get; set; }
    public string? UpdaterUserId { get; set; }
    public Status Status { get; set; }
}