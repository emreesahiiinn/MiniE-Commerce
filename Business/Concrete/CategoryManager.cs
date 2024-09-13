using Business.Abstract.Services;
using DataAccess.Abstract.Services;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager(ICategoryDal categoryDal) : ICategoryService
{
    public List<Category> GetAll()
    {
        return categoryDal.GetAll();
    }

    public Category GetById(int categoryId)
    {
        return categoryDal.Get(x => x.CategoryId == categoryId);
    }
}