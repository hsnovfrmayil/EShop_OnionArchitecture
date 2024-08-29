using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.Behaviors.Query.Product.GetAll;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    //private readonly IMediator _mediator;

    public ProductController(IProductService productService)
    {
        _productService = productService;
        //_mediator = mediator;
    }

    [HttpGet("AllProducts")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationVM paginationVM)
    {
        var allProdudctVM = await _productService.GetAllProductsAsync(paginationVM);
        //return Ok(productForPage);
        return Ok(allProdudctVM);
    }

    //[HttpGet("AllProducts")]
    //public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest request)
    //{
    //    GetAllProductQueryResponse response =await _mediator.Send(request);
    //    //return Ok(productForPage);
    //    //if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
    //    //    return NotFound("Product not Found");
    //    return response.Products.Count == 0 ? NotFound("Product not Found") : Ok(response.Products);
    //}

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

