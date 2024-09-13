using Business.Abstract.Services;
using Core.Entities.Concrete;
using DataAccess.Abstract.Services;

namespace Business.Concrete;

public class UserManager(IUserDal userDal) : IUserService
{
    public async Task<List<OperationClaim>> GetClaims(User user)
    {
        return await userDal.GetClaims(user);
    }

    public async Task Add(User user)
    {
        await userDal.AddAsync(user);
    }

    public async Task<User> GetByMail(string email)
    {
        return await userDal.GetAsync(u => u.Email == email);
    }
}