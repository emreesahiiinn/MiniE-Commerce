using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Services;

public interface IUserDal : IEntityRepository<User>, IDataAccessService
{
    Task<List<OperationClaim>> GetClaims(User user);
}