using Common.Query;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Shop.Query.Report.Order.Dtos;

namespace Shop.Query.Report.Order.GetNewOrder
{
    internal class GetRecentOrdersQueryHandler : IRequestHandler<GetRecentOrdersQuery, List<RecentOrderDto>>
    {
        private readonly ShopContext _context;

        public GetRecentOrdersQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<RecentOrderDto>> Handle(GetRecentOrdersQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                 .Include(p => p.ProductVariants)
                 .ToListAsync(cancellationToken);

            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Address)
                .Include(o => o.Items)
                .Where(o => o.Status == OrderStatus.Finally)
                .OrderByDescending(o => o.CreationDate)
                .Take(20)
                .ToListAsync(cancellationToken);

            var result = orders.Select(order => new RecentOrderDto
            {
                Id = order.Id,
                City = order.Address.City,
                PhoneNumber = order.Address.PhoneNumber,
                PaidMoney = order.TotalPrice,
                OrderDate = order.CreationDate,
                Items = order.Items.Select(item =>
                {
                    var product = products.FirstOrDefault(p =>
                        p.ProductVariants.Any(v => v.Id == item.ProductVariantId));

                    return new RecentOrderItemDto
                    {
                        ProductTitle = product?.Title,
                        ProductImageName = product?.ImageName,
                        Count = item.Count,
                        Price = item.Price
                    };
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
