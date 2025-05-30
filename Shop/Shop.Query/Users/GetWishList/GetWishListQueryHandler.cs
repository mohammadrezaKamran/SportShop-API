using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products;
using Shop.Query.Products.DTOs;
using Shop.Query.Users.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetWishList
{
    internal class GetWishListQueryHandler : IQueryHandler<GetWishListQuery, List<WishListDto>>
    {
        private readonly ShopContext _context;

        public GetWishListQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<WishListDto>> Handle(GetWishListQuery request, CancellationToken cancellationToken)
        {
            var productIds = await _context.Users.Where(u => u.Id == request.userId)
                                            .SelectMany(u => u.WishLists).Select(w => w.ProductId).ToListAsync(cancellationToken);

            if (!productIds.Any())
                return new List<WishListDto>();


            return await _context.Products
        .Where(p => productIds.Contains(p.Id))
        .Select(p => new WishListDto
        {
            Id = p.Id,
            Title = p.Title,
            ImageName = p.ImageName,
            Slug = p.Slug,
            Status = p.Status,
            
            Price = p.ProductVariants.Min(d => d.Price),
            StockQuantity = p.ProductVariants
                      .Where(v => v.StockQuantity > 0)
                      .Select(v => v.StockQuantity)
                      .FirstOrDefault(),
        })
        .ToListAsync(cancellationToken);

        }
    }
}
