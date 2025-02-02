﻿using System;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities.Concretes;

public class Category :BaseEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public ICollection<Product> Products { get; set; }
}

