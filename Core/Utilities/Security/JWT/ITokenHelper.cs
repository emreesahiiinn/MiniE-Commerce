using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT;

public interface ITokenHelper : ICoreService
{
    AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
}