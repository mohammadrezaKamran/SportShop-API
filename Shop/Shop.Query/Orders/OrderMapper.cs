using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders;

internal static class OrderMapper
{
    public static OrderDto Map(this Order order)
    {
        return new OrderDto()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            Status = order.Status,
            Address = order.Address,
            Discount = order.Discount,
            Items = new(),
            LastUpdate = order.LastUpdate,
            ShippingMethod = order.ShippingMethod,
            UserFullName = "",
            UserId = order.UserId,
            OrderNumber = order.OrderNumber,
            TrackingNumber = order.TrackingNumber,
        };
    }

    public static async Task<List<OrderItemDto>> GetOrderItems(this OrderDto orderDto, DapperContext dapperContext)
    {
        using var connection = dapperContext.CreateConnection();

        var sql = @$"
        SELECT 
            o.Id,
            o.OrderId,
            o.ProductVariantId,
            o.Count,
            o.Price,
            p.Title as ProductTitle,
            p.Slug as ProductSlug,
            p.ImageName as ProductImageName
        FROM {dapperContext.OrderItems} o
        INNER JOIN {dapperContext.ProductVariants} pv ON o.ProductVariantId = pv.Id
        INNER JOIN {dapperContext.Products} p ON pv.ProductId = p.Id
        WHERE o.OrderId = @orderId";

        var result = await connection.QueryAsync<OrderItemDto>(sql, new { orderId = orderDto.Id });

        return result.ToList();
    }
    public static OrderFilterData MapFilterData(this Order order, ShopContext context)
    {
        var userFullName = context.Users
            .Where(r => r.Id == order.UserId)
            .Select(u => $"{u.Name} {u.Family}")
            .First();

        return new OrderFilterData()
        {
            Status = order.Status,
            Id = order.Id,
            CreationDate = order.CreationDate,
            City = order.Address?.City,
            ShippingType = order.ShippingMethod?.ShippingType,
            Shire = order.Address?.Shire,
            TotalItemCount = order.ItemCount,
            TotalPrice = order.TotalPrice,
            UserFullName = userFullName,
            UserId = order.UserId,
            OrderNumber=order.OrderNumber,
            TrackingNumber=order.TrackingNumber,
        };
    }
}