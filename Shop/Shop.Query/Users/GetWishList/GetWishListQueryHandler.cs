using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetWishList
{
    internal class GetWishListQueryHandler : IQueryHandler<GetWishListQuery,List<ProductDto?>>
    {
        private readonly ShopContext _context;

        public GetWishListQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto?>> Handle(GetWishListQuery request, CancellationToken cancellationToken)
        {
            var productIds = await _context.Users.Where(u=>u.Id==request.userId)
                                            .SelectMany(u => u.WishLists).Select(w=>w.ProductId).ToListAsync(cancellationToken);

            if (!productIds.Any())
                return null;


            return await _context.Products
                            .Where(p => productIds.Contains(p.Id)).Select(s=>s.Map())
                            .ToListAsync(cancellationToken);

        }
    }
}
