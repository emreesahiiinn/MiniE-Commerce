using Core.Entities.Abstract;
using Core.Entities.Model;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract.Services;

public interface IProductService : IBusinessService
{
    Task<IDataResult<PagedResult<Product>>> GetAll();
    Task<IDataResult<PagedResult<Product>>> GetAllByCategoryId(int id);
    Task<IDataResult<PagedResult<Product>>> GetAllByUnitPrice(decimal minPrice, decimal maxPrice);
    Task<IDataResult<Product>> GetById(int productId);
    IDataResult<List<ProductDetailDto>> GetProductDetails();
    Task<IResult> Add(Product product);
}