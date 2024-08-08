using System;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Persistence.DbContext;
using ECommerce.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Repositories;

public class ReadCategoryRepository : ReadGenericRepository<Category>,IReadCategoryRepository
{
    public ReadCategoryRepository(ECommerceDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> AllProductByCategory(int categoryId)
    {
        //var products = _context.Products.Where(x => x.Id == categoryId);
        //var products = await _table.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId).Products;
        var category = await _table.Include(x => x.Products)
                           .FirstOrDefaultAsync(x => x.Id == categoryId);

        var products = category?.Products;


        return products;
    }
}

