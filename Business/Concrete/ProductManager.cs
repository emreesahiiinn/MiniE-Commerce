using Entities.DTOs;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour == 18)
        {
            return new ErrorDataResult<List<Product>>(Messages.Error);
        }

        return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.Success);
    }

    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.CategoryId == id), Messages.Success);
    }

    public IDataResult<List<Product>> GetAllByUnitPrice(decimal minPrice, decimal maxPrice)
    {
        return new SuccessDataResult<List<Product>>(
            _productDal.GetAll(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice), Messages.Success);
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.Success);
    }

    public IDataResult<Product> GetById(int productId)
    {
        return new SuccessDataResult<Product>(_productDal.Get(x => x.ProductId == productId), Messages.Success);
    }

    public IResult Add(Product product)
    {
        if (product.ProductName.Length < 2)
        {
            return new ErrorResult(Messages.ProductInvalidName);
        }

        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }
}