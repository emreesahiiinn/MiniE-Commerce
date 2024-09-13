using Business.Abstract.Services;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract.Services;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class ProductManager(IProductDal productDal) : IProductService
{
    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour == 18) return new ErrorDataResult<List<Product>>(Messages.Error);

        return new SuccessDataResult<List<Product>>(productDal.GetAll(), Messages.Success);
    }

    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(productDal.GetAll(x => x.CategoryId == id), Messages.Success);
    }

    public IDataResult<List<Product>> GetAllByUnitPrice(decimal minPrice, decimal maxPrice)
    {
        return new SuccessDataResult<List<Product>>(
            productDal.GetAll(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice), Messages.Success);
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(productDal.GetProductDetails(), Messages.Success);
    }

    [SecuredOperation("admin")]
    public IDataResult<Product> GetById(int productId)
    {
        return new SuccessDataResult<Product>(productDal.Get(x => x.ProductId == productId), Messages.Success);
    }

    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {
        var result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
            CheckIfProductNamesSame(product.ProductName));
        if (result is not null) return result;
        productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = productDal.GetAll(x => x.CategoryId == categoryId).Count;
        if (result >= 15) return new ErrorResult(Messages.Error);

        return new SuccessResult(Messages.Success);
    }

    private IResult CheckIfProductNamesSame(string productName)
    {
        var result = productDal.GetAll(x => x.ProductName == productName).Any();
        if (result) return new ErrorResult(Messages.Error);

        return new SuccessResult(Messages.Success);
    }
}