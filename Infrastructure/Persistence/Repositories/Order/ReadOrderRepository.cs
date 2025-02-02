﻿using System;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Persistence.DbContext;
using ECommerce.Persistence.Repositories.Common;

namespace ECommerce.Persistence.Repositories;

public class ReadOrderRepository : ReadGenericRepository<Order>, IReadOrderRepository
{
    public ReadOrderRepository(ECommerceDbContext context) : base(context)
    {
    }
}

