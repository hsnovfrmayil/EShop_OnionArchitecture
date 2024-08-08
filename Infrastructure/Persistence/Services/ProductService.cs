using System;
using System.Net;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Domain.ViewModels;
using ECommerce.Persistence.Repositories;
using ECommerce.Application.Extentions;

namespace ECommerce.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IReadProductRepository _readProductRepository;
    private readonly IWriteProductRepository _writeProductRepository;
    private readonly IReadCategoryRepository _readCategoryRepository;

    public ProductService(IReadProductRepository readProductRepository, IWriteProductRepository writeProductRepository,
        IReadCategoryRepository readCategoryRepository)
    {
        _readProductRepository = readProductRepository;
        _writeProductRepository = writeProductRepository;
        _readCategoryRepository = readCategoryRepository;
    }

    public async Task AddProductAsync(AddProductVM addProductVM)
    {
        var product = new Product()
        {
            Name = addProductVM.Name,
            Price = addProductVM.Price,
            Description = addProductVM.Description,
            CategoryId = addProductVM.CategoryId
        };

        await _writeProductRepository.AddAsync(product);
        await _writeProductRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _readProductRepository.GetByIdAsync(productId);

        if (product == null)
            //return NotFound();
            return false;

        await _writeProductRepository.DeleteAsync(product);
        await _writeProductRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<GetProductVM>> GetAllProductsAsync(PaginationVM paginationVM)
    {
        var products = await _readProductRepository.GetAllAsync();
        //var productForPage = products.ToList().Skip(paginationVM.Page * paginationVM.Size).Take(paginationVM.Size);
        var productForPage = products.Paginate(paginationVM.Page);

        var allProdudctVM = productForPage.Select(p => new GetProductVM()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description,
            CategoryName = p.Category != null ? p.Category.Name : "Unknown",
            ImageUrl = p.ImageUrl,
            Stock = p.Stock
        }).ToList();

        return allProdudctVM;
    }

    public async Task<GetProductVM?> GetProductByIdAsync(int productId)
    {
        var product = await _readProductRepository.GetByIdAsync(productId);

        if (product == null)
            return null;

        var productVM = new GetProductVM()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryName = product.Category != null ? product.Category.Name : "Unknown",
            ImageUrl = product.ImageUrl,
            Stock = product.Stock
        };

        return productVM;
    }

    public async Task<HttpStatusCode> UpdateProductAsync(int id, UpdateProductVM productVM)
    {
        var product = await _readProductRepository.GetByIdAsync(id);
        if (product == null)
            return HttpStatusCode.NotFound;
        var category = await _readCategoryRepository.GetByIdAsync(productVM.CategoryId);

        if (category == null)
            return HttpStatusCode.NotFound;

        product.Name = productVM.Name;
        product.Price = productVM.Price;
        product.Description = productVM.Description;
        product.CategoryId = productVM.CategoryId;

        await _writeProductRepository.UpdateAsync(product);
        await _writeProductRepository.SaveChangesAsync();

        return HttpStatusCode.OK;
    }
}

