using Business.Abstract.Services;
using Business.Constants;
using Core.Entities.Abstract;
using Core.Entities.Model;
using Core.Utilities.Results;
using DataAccess.Abstract.Services;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager(ICategoryDal categoryDal) : ICategoryService
{
    public async Task<IDataResult<PagedResult<Category>>> GetAll(int? page, int? pageSize)
    {
        var result = await categoryDal.GetAllAsync(null, page ?? 1, pageSize ?? 10);
        return new SuccessDataResult<PagedResult<Category>>(result, Messages.Success);
    }

    public async Task<IDataResult<Category>> GetById(string categoryId)
    {
        var result = await categoryDal.GetAsync(x => x.Id.ToString() == categoryId);
        return new SuccessDataResult<Category>(result, Messages.Success);
    }
}