using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory;

public class InMemoryProductDal : IProductDal
{
    private List<Product> _products;

    public InMemoryProductDal()
    {
        _products = new List<Product>
        {
            new Product { ProductId = 1, CategoryId = 1, ProductName = "Pencil", UnitPrice = 10, UnitsInStock = 100 },
            new Product { ProductId = 2, CategoryId = 1, ProductName = "Phone", UnitPrice = 1000, UnitsInStock = 25 },
            new Product { ProductId = 3, CategoryId = 1, ProductName = "Pc", UnitPrice = 3000, UnitsInStock = 100 },
            new Product { ProductId = 4, CategoryId = 1, ProductName = "Xbox", UnitPrice = 2500, UnitsInStock = 100 },
            new Product { ProductId = 5, CategoryId = 1, ProductName = "Keyboard", UnitPrice = 150, UnitsInStock = 15 },
        };
    }

    public List<Product> GetAll()
    {
        return _products;
    }

    public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public Product Get(Expression<Func<Product, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Update(Product product)
    {
        Product productToUpdate = _products.SingleOrDefault(x => x.ProductId == product.ProductId);
        if (productToUpdate != null)
        {
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
        else
        {
            Console.WriteLine("Product Not Found");
        }
    }

    public void Delete(Product product)
    {
        Product productToDelete = null;

        // Without using LINQ
     
        // foreach (var x in _products)
        // {
        //     if (x.ProductId == product.ProductId)
        //     {
        //         productToDelete = product;
        //     }
        // }

        // Using LİNQ

        productToDelete = _products.SingleOrDefault(x => x.ProductId == product.ProductId);

        if (productToDelete != null)
        {
            _products.Remove(productToDelete);
        }
        else
        {
            Console.WriteLine("Product Not Found");
        }
    }

    public List<ProductDetailDto> GetProductDetails()
    {
        throw new NotImplementedException();
    }
}