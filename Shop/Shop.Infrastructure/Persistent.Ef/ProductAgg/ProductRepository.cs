using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure._Utilities;
using System;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ShopContext context) : base(context)
    {
    }

    public async Task<ProductVariant?> GetVariantById(long variantId)
    {
        var product = await Context.Products
     .Include(p => p.ProductVariants)
     .FirstOrDefaultAsync(p => p.ProductVariants.Any(v => v.Id == variantId));

        return product?.ProductVariants.FirstOrDefault(v => v.Id == variantId);
    }

}