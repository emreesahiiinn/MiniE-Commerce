using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDal : EfEntityRepositoryBase<Product, MiniECommerceDbContext>, IProductDal
{
    public List<ProductDetailDto> GetProductDetails()
    {
        using (MiniECommerceDbContext context = new MiniECommerceDbContext())
        {
            var result = from p in context.Products
                join c in context.Categories
                    on p.CategoryId equals c.CategoryId
                select new ProductDetailDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryName = c.CategoryName,
                    UnitsInStock = p.UnitsInStock,
                };
            return result.ToList();
        }
    }
}