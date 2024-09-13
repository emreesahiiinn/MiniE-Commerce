using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Services;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.EFDal;

public class EfOrderDal : EfEntityRepositoryBase<Order, MiniECommerceContext>, IOrderDal
{
}