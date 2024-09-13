using Business.Abstract.Services;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Abstract;
using Core.Entities.Model;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract.Services;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class ProductManager(IProductDal productDal) : IProductService
{
    public async Task<IDataResult<PagedResult<Product>>> GetAll()
    {
        return new SuccessDataResult<PagedResult<Product>>(await productDal.GetAllAsync(), Messages.Success);
    }

    public async Task<IDataResult<PagedResult<Product>>> GetAllByCategoryId(int id)
    {
        return new SuccessDataResult<PagedResult<Product>>(await productDal.GetAllAsync(x => x.CategoryId == id),
            Messages.Success);
    }

    public async Task<IDataResult<PagedResult<Product>>> GetAllByUnitPrice(decimal minPrice, decimal maxPrice)
    {
        return new SuccessDataResult<PagedResult<Product>>(
            await productDal.GetAllAsync(x => x.UnitPrice >= minPrice && x.UnitPrice <= maxPrice), Messages.Success);
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(productDal.GetProductDetails(), Messages.Success);
    }

    [SecuredOperation("admin")]
    public async Task<IDataResult<Product>> GetById(int productId)
    {
        return new SuccessDataResult<Product>(await productDal.GetAsync(x => x.ProductId == productId),
            Messages.Success);
    }

    [ValidationAspect(typeof(ProductValidator))]
    public async Task<IResult> Add(Product product)
    {
        var result = BusinessRules.Run(await CheckIfProductCountOfCategoryCorrect(product.CategoryId),
            await CheckIfProductNamesSame(product.ProductName));
        if (!result.Status) return result;
        await productDal.AddAsync(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    private async Task<IResult> CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = await productDal.GetAllAsync(x => x.CategoryId == categoryId);
        if (result.Records.Count >= 15) return new ErrorResult(Messages.Error);

        return new SuccessResult(Messages.Success);
    }

    private async Task<IResult> CheckIfProductNamesSame(string productName)
    {
        var result = await productDal.GetAllAsync(x => x.ProductName == productName);
        if (result.Records.Any()) return new ErrorResult(Messages.Error);

        return new SuccessResult(Messages.Success);
    }
}