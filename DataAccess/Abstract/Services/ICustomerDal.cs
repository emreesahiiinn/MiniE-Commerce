using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Services;

public interface ICustomerDal : IEntityRepository<Customer>, IDataAccessService
{
}