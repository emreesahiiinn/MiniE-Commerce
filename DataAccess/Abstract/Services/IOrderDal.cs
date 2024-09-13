using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Services;

public interface IOrderDal : IEntityRepository<Order>, IDataAccessService
{
}