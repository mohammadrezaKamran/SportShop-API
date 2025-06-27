using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report
{
    public class GetNumberOfThingsQuery:IRequest<NumberOfThingDto>
    {
    }
}
public class GetNumberOfThingsQueryHandler : IRequestHandler<GetNumberOfThingsQuery, NumberOfThingDto>
{
    private readonly ShopContext _context;

    public GetNumberOfThingsQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<NumberOfThingDto> Handle(GetNumberOfThingsQuery request, CancellationToken cancellationToken)
    {
        var numberOfProducts = await _context.Products.CountAsync(cancellationToken);

        var productVariants = await _context.Products
            .SelectMany(p => p.ProductVariants)
            .ToListAsync(cancellationToken);

        var numberOfOutOfStock = productVariants.Count(v => v.StockQuantity == 0);
        var numberOfLowStock = productVariants.Count(v => v.StockQuantity > 0 && v.StockQuantity < 5);

        var numberOfComments = await _context.Comments.CountAsync(cancellationToken);
        var numberOfUsers = await _context.Users.CountAsync(cancellationToken);

        var numberOfNewOrders = await _context.Orders
            .CountAsync(o => o.Status == OrderStatus.Finally, cancellationToken);

        return new NumberOfThingDto
        {
            NumberOfProducts = numberOfProducts,
            NumberOfOutOfStuckProducts = numberOfOutOfStock,
            NumberOfLowStuckProducts = numberOfLowStock,
            NumberOfComments = numberOfComments,
            NumberOfUsers = numberOfUsers,
            NumberOfNewOrders = numberOfNewOrders
        };
    }
}
public class NumberOfThingDto
{
    public int NumberOfProducts {  get; set; }
    public int NumberOfOutOfStuckProducts { get; set; }
    public int NumberOfLowStuckProducts { get; set; }
    public int NumberOfComments { get; set; }
    public int NumberOfUsers { get; set; }
    public int NumberOfNewOrders { get; set; }
}