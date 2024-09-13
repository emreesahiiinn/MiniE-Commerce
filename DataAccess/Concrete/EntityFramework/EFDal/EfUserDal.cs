using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract.Services;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.EFDal;

public class EfUserDal : EfEntityRepositoryBase<User, MiniECommerceContext>, IUserDal
{
    public async Task<List<OperationClaim>> GetClaims(User user)
    {
        await using var context = new MiniECommerceContext();
        var result = from operationClaim in context.OperationClaims
            join userOperationClaim in context.UserOperationClaims
                on operationClaim.Id equals userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == user.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
        return await result.ToListAsync();
    }
}