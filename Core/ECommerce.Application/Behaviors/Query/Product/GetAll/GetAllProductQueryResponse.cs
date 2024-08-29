using System;
using System.Net;
using ECommerce.Domain.ViewModels;

namespace ECommerce.Application.Behaviors.Query.Product.GetAll;

public class GetAllProductQueryResponse
{
    public ICollection<GetProductVM> Products { get; set; }
}

