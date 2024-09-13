using Core.Entities.Abstract;

namespace Core.Entities.Concrete;

public class User : BaseEntity, IEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}