using System;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Abstracts;

namespace ECommerce.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : IBaseEntity, new()
{
    public Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task Update(T entity)
    {
        throw new NotImplementedException();
    }
}

