using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("AllProducts")]
    public async Task<IActionResult> GetAll([FromQuery]PaginationVM paginationVM)
    {
        var allProdudctVM = await _productService.GetAllProductsAsync(paginationVM);
        //return Ok(productForPage);
        return Ok(allProdudctVM);
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody]AddProductVM addProductVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _productService.AddProductAsync(addProductVM);
        return StatusCode(201);
    }

    //Delete product By Id
    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute]int id)
    {
        if (await _productService.DeleteProductAsync(id) == false)
            return NotFound();

        return StatusCode(204);
    }

    //Update product by id
    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductVM productVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _productService.UpdateProductAsync(id, productVM);

        return StatusCode((int)result);
    }


    [HttpGet("GetProduct/{id}")]

    public async Task<IActionResult> GetProduct(int id)
    {
       var productVM = await _productService.GetProductByIdAsync(id);
        if (productVM == null)
            return NotFound("Product not Found");
        return Ok(productVM);
    }
  
}

