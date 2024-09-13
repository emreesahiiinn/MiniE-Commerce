using Business.Abstract.Services;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : Controller
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await productService.GetAll();
        if (result.Status) return Ok(result.Data);

        return BadRequest(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await productService.GetById(id);
        if (result.Status) return Ok(result.Data);

        return BadRequest(result);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Post(Product product)
    {
        var result = await productService.Add(product);
        if (result.Status) return Ok(result);

        return BadRequest(result);
    }
}