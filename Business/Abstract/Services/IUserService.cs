using Core.Entities.Concrete;

namespace Business.Abstract.Services;

public interface IUserService : IBusinessService
{
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    User GetByMail(string email);
}