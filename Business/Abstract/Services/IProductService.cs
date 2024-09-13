using Core.Entities.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract.Services;

public interface IProductService : IBusinessService
{
    IDataResult<List<Product>> GetAll();
    IDataResult<List<Product>> GetAllByCategoryId(int id);
    IDataResult<List<Product>> GetAllByUnitPrice(decimal minPrice, decimal maxPrice);
    IDataResult<Product> GetById(int productId);
    IDataResult<List<ProductDetailDto>> GetProductDetails();
    IResult Add(Product product);
}