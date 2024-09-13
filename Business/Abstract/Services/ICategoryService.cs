using Entities.Concrete;

namespace Business.Abstract.Services;

public interface ICategoryService : IBusinessService
{
    List<Category> GetAll();
    Category GetById(int categoryId);
}