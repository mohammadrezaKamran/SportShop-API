using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.ShopReport.ProductReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.ShopReport.ProductReport.GetAllProductReport
{
    public class GetAllProductReportQueryHandler : IQueryHandler<GetAllProductReportQuery, List<ProductReportDto>>
    {
        private readonly ShopContext _context;

        public GetAllProductReportQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductReportDto>> Handle(GetAllProductReportQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.ProductVariants)
                .ToListAsync(cancellationToken);

            var orders = await _context.Orders
                .Where(o => o.Status == OrderStatus.Finally)
                .Include(o => o.Items)
                .ToListAsync(cancellationToken);

            var report = products.Select(p => new ProductReportDto
            {
                ProductId = p.Id,
                Title = p.Title,
                Variants = p.ProductVariants.Select(variant =>
                {
                    var variantOrders = orders.SelectMany(o => o.Items)
                        .Where(i => i.ProductVariantId == variant.Id);

                    return new ProductVariantReportDto
                    {
                        VariantId = variant.Id,
                        Color = variant.Color,
                        Size = variant.Size,
                        TotalSold = variantOrders.Sum(i => i.Count),
                        TotalRevenue = variantOrders.Sum(i => i.TotalPrice)
                    };
                }).ToList()
            }).ToList();

            return report;
        }
    }
}
