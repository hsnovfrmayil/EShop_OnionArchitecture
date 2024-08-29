using System;
using MediatR;

namespace ECommerce.Application.Behaviors.Query.Product.GetAll;

public class GetAllProductQueryRequest :IRequest<GetAllProductQueryResponse>
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 10;
}

