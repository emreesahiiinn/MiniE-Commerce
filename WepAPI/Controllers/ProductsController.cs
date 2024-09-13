using Business.Abstract.Services;
using Core.Entities.Abstract;
using Core.Entities.Model;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Entities.Abstract.IResult;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : Controller
{

    [HttpGet]
    public async Task<IDataResult<PagedResult<Product>>> Get(int? page, int? pageSize)
    {
        var result = await productService.GetAll();
        HttpContext.Items["Result"] = result;
        return result;
    }

    [HttpGet("GetById")]
    public async Task<IDataResult<Product>> Get(int id)
    {
        var result = await productService.GetById(id);
        HttpContext.Items["Result"] = result;
        return result;
    }

    [HttpPost("Add")]
    public async Task<IResult> Post(Product product)
    {
        var result = await productService.Add(product);
        HttpContext.Items["Result"] = result;
        return result;
    }
}