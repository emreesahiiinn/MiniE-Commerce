using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Services;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework.EFDal;

public class EfProductDal : EfEntityRepositoryBase<Product, MiniECommerceContext>, IProductDal
{
    public List<ProductDetailDto> GetProductDetails()
    {
        using var context = new MiniECommerceContext();
        var result = from p in context.Products
            join c in context.Categories
                on p.CategoryId equals c.CategoryId
            select new ProductDetailDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryName = c.CategoryName,
                UnitsInStock = p.UnitsInStock
            };
        return result.ToList();
    }
}