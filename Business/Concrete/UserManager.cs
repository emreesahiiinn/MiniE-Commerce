using Business.Abstract.Services;
using Core.Entities.Concrete;
using DataAccess.Abstract.Services;

namespace Business.Concrete;

public class UserManager(IUserDal userDal) : IUserService
{
    public List<OperationClaim> GetClaims(User user)
    {
        return userDal.GetClaims(user);
    }

    public void Add(User user)
    {
        userDal.Add(user);
    }

    public User GetByMail(string email)
    {
        return userDal.Get(u => u.Email == email);
    }
}