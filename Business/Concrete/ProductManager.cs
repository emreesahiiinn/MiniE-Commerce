using Entities.DTOs;
using Entities.Concrete;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Business;

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

    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {
        IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
            CheckIfProductNamesSame(product.ProductName));
        if (result is not null)
        {
            return result;
        }

        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
        if (result >= 15)
        {
            return new ErrorResult(Messages.Error);
        }

        return new SuccessResult(Messages.Success);
    }

    private IResult CheckIfProductNamesSame(string productName)
    {
        var result = _productDal.GetAll(x => x.ProductName == productName).Any();
        if (result)
        {
            return new ErrorResult(Messages.Error);
        }

        return new SuccessResult(Messages.Success);
    }
}