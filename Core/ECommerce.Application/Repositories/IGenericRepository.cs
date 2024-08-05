using System;
using ECommerce.Domain.Entities.Abstracts;

namespace ECommerce.Application.Repositories;

public interface IGenericRepository<T> where T:IBaseEntity, new()
{
	Task<List<T>> GetAllAsync();

	Task<T> GetByIdAsync(int id);

	Task<T> AddAsync(T entity);

	Task Update(T entity);

	Task Remove(int id);

	Task SaveChangesAsync();
}

