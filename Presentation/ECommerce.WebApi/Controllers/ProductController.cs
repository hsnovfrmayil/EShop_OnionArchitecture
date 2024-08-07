using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers;

[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IReadProductRepository _readProductRepository;
    private readonly IWriteProductRepository _writeProductRepository;


    public ProductController(IReadProductRepository readProductRepository, IWriteProductRepository writeProductRepository)
    {
        _readProductRepository = readProductRepository;
        _writeProductRepository = writeProductRepository;
    }

    [HttpGet("AllProducts")]
    public async Task<IActionResult> GetAll([FromQuery]PaginationVM paginationVM)
    {
        var products =await _readProductRepository.GetAllAsync();
        var productForPage= products.ToList().Skip(paginationVM.Page * paginationVM.Size).Take(paginationVM.Size);

        return Ok(productForPage);
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody]AddProductVM addProductVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = new Product()
        {
            Name = addProductVM.Name,
            Price=addProductVM.Price,
            Description=addProductVM.Description,
            CategoryId=addProductVM.CategoryId,
            CreatedAt=DateTime.Now,
            UpdatedAt=DateTime.Now
        };

        await _writeProductRepository.AddAsync(product);
        await _writeProductRepository.SaveChangesAsync();
        return StatusCode(201);
    }

  
}

