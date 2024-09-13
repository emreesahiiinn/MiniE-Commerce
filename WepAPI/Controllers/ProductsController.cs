using Business.Abstract.Services;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : Controller
{

    [HttpGet]
    public IActionResult Get()
    {
        var result = productService.GetAll();
        if (result.Status) return Ok(result.Data);

        return BadRequest(result);
    }

    [HttpGet("GetById")]
    public IActionResult Get(int id)
    {
        var result = productService.GetById(id);
        if (result.Status) return Ok(result.Data);

        return BadRequest(result);
    }

    [HttpPost("Add")]
    public IActionResult Post(Product product)
    {
        var result = productService.Add(product);
        if (result.Status) return Ok(result);

        return BadRequest(result);
    }
}