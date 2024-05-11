using Core.DataAccess.EntityFramework;
using Core.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.EFDal;

public class EfCategoryDal : EfEntityRepositoryBase<Category, MiniECommerceContext>, ICategoryDal
{
}