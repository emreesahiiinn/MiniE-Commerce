using Core.Entities.Abstract;
using Core.Entities.Model;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract.Services;

public interface ICategoryService : IBusinessService
{
    Task<IDataResult<PagedResult<Category>>> GetAll(int? page, int? pageSize);
    Task<IDataResult<Category>> GetById(string categoryId);
}