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
public class CategoryController : Controller
{
    private readonly IReadCategoryRepository _readCategorytRepository;
    private readonly IWriteCategoryRepository _writeCategoryRepository;


    public CategoryController(IReadCategoryRepository readCategorytRepository, IWriteCategoryRepository writeCategoryRepository)
    {
        _readCategorytRepository = readCategorytRepository;
        _writeCategoryRepository = writeCategoryRepository;
    }

    [HttpGet("AddCategory")]
    public async Task<IActionResult> AddCategory([FromBody]AddCategoryVM addCategoryVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var category = new Category()
        {
            Name = addCategoryVM.Name,
            Description = addCategoryVM.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _writeCategoryRepository.AddAsync(category);
        await _writeCategoryRepository.SaveChangesAsync();
        return StatusCode(201);
    }

  
}

