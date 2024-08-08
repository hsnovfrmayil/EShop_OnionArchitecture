using System;
using System.Net;
using ECommerce.Domain.ViewModels;

namespace ECommerce.Application.Services;

public interface IProductService
{
    Task<IEnumerable<GetProductVM>> GetAllProductsAsync(PaginationVM paginationVM);
    Task<GetProductVM?> GetProductByIdAsync(int productId);
    Task AddProductAsync(AddProductVM addProductVM);
    Task<HttpStatusCode> UpdateProductAsync(int id,UpdateProductVM updateProductVM);
    Task<bool> DeleteProductAsync(int productId);
}
