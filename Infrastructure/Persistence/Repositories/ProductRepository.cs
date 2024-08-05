using System;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Concretes;

namespace ECommerce.Persistence.Repositories;

public class ProductRepository:GenericRepository<Product>,IProductRepository
{

}

