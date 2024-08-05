using System;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities.Concretes;

public class Product: BaseEntity
{
	public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }

}

