using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
using Shop.Query.Report.ProductReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.ProductReport.GetOutofStuckProduct
{
    internal class GetOutOfStockProductsQueryHandler : IQueryHandler<GetOutOfStockProductsQuery, List<OutOfStockProductDto>>
    {
        private readonly ShopContext _context;

        public GetOutOfStockProductsQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<OutOfStockProductDto>> Handle(GetOutOfStockProductsQuery request, CancellationToken cancellationToken)
        {
     
            return await _context.Products
                  .Include(p => p.ProductVariants)
                  .SelectMany(p => p.ProductVariants
                      .Where(v => v.StockQuantity <= 3)
                      .Select(v => new OutOfStockProductDto
                      {
                          ProductId = p.Id,
                          ImageName = p.ImageName,
                          Title = p.Title,
                          Slug = p.Slug,
                          SKU = v.SKU,
                          Size = v.Size,
                          Color = v.Color,
                          StockQuantity = v.StockQuantity
                      }))
                  .ToListAsync(cancellationToken);
        }
    }
}
