using System;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Persistence.DbContext;
using ECommerce.Persistence.Repositories.Common;

namespace ECommerce.Persistence.Repositories;

public class WriteProductRepository : WriteGenericRepository<Product>, IWriteProductRepository
{
    public WriteProductRepository(ECommerceDbContext context) : base(context)
    {
    }
}

