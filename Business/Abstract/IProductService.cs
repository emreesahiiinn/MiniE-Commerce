using Core.Entities.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IProductService
{
    IDataResult<List<Product>> GetAll();
    IDataResult<List<Product>> GetAllByCategoryId(int id);
    IDataResult<List<Product>> GetAllByUnitPrice(decimal minPrice, decimal maxPrice);
    IDataResult<Product> GetById(int productId);
    IDataResult<List<ProductDetailDto>> GetProductDetails();
    IResult Add(Product product);
}