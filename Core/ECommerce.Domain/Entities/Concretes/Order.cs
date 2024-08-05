using System;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities.Concretes;

public class Order :BaseEntity
{
	public string? OrderName { get; set; }

	public DateTime OrderDate { get; set; }

	public string OrderNote { get; set; }

	public decimal Total { get; set; }

	public int CustomerId { get; set; }

    public virtual ICollection<Product> Products { get; set; }

	public virtual Customer Customer { get; set; }
}

