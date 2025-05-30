using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Report.ProductReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.ProductReport.GetBestSellersProduct
{
    internal class GetBestSellersQueryHandler : IQueryHandler<GetBestSellersQuery, List<ProductSalesDto>>
    {
        private readonly ShopContext _context;

        public GetBestSellersQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSalesDto>> Handle(GetBestSellersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var itemData = await _context.Orders
               .SelectMany(order => order.Items)
               .GroupBy(i => i.ProductVariantId)
               .Select(g => new
               {
                   ProductVariantId = g.Key,
                   TotalSold = g.Sum(i => i.Count)
               })
               .ToListAsync(cancellationToken);

                var result = await _context.Products
                    .SelectMany(p => p.ProductVariants, (product, variant) => new { product, variant })
                    .Where(x => itemData.Select(d => d.ProductVariantId).Contains(x.variant.Id))
                    .Select(x => new ProductSalesDto
                    {
                        ProductId = x.product.Id,
                        SKU = x.variant.SKU,
                        TotalSold = itemData.First(d => d.ProductVariantId == x.variant.Id).TotalSold
                    })
                    .OrderByDescending(x => x.TotalSold)
                    .Take(10)
                    .ToListAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
