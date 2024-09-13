using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract.Services;

public interface ICategoryDal : IEntityRepository<Category>, IDataAccessService
{
}