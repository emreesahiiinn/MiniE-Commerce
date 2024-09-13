using Core.Entities.Concrete;

namespace Business.Abstract.Services;

public interface IUserService : IBusinessService
{
    Task<List<OperationClaim>> GetClaims(User user);
    Task Add(User user);
    Task<User> GetByMail(string email);
}