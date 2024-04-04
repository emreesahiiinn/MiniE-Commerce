using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _productService.GetAll();
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result);
    }

    [HttpGet("GetById")]
    public IActionResult Get(int id)
    {
        var result = _productService.GetById(id);
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result);
    }

    [HttpPost("Add")]
    public IActionResult Post(Product product)
    {
        var result = _productService.Add(product);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}