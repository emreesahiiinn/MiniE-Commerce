using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Entities.Concrete;

public class User : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
}