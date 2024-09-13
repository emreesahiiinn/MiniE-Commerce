using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract.Services;

public interface IProductDal : IEntityRepository<Product>, IDataAccessService
{
    List<ProductDetailDto> GetProductDetails();
}