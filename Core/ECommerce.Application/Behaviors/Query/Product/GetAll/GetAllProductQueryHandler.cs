using System;
using ECommerce.Application.Extentions;
using ECommerce.Application.Repositories;
using ECommerce.Domain.ViewModels;
using MediatR;
namespace ECommerce.Application.Behaviors.Query.Product.GetAll;
//MediatR
public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IReadProductRepository _readProductRepository;

    public GetAllProductQueryHandler(IReadProductRepository readProductRepository)
    {
        _readProductRepository = readProductRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _readProductRepository.GetAllAsync();
        //var productForPage = products.ToList().Skip(paginationVM.Page * paginationVM.Size).Take(paginationVM.Size);
        //var productForPage = products.Paginate(request.Page);
        var productForPage = products.Paginate(request.Page);


        var allProdudctVM = productForPage.Select(p => new GetProductVM()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description,
            CategoryName = p.Category != null ? p.Category.Name : "Unknown",
            ImageUrl = p.ImageUrl,
            Stock = p.Stock
        }).ToList();

        return new GetAllProductQueryResponse()
        {
            Products=allProdudctVM
        };
    }
}

