using System;
namespace ECommerce.Domain.ViewModels;

//public record PaginationVM
//{
//    public int Page { get; init; } = 0;
//    public int Size { get; init; } = 10;
//}


public class PaginationVM
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 10;
}

