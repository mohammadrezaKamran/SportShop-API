using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetOrderItems;
using System;

public class GetOrderItemsQueryHandler : IQueryHandler<GetOrderItemsQuery, List<OrderItemDto>>
{
    private readonly ShopContext _context;

    public GetOrderItemsQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<OrderItemDto>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}
