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
     .Include(p => p.ProductVariants).AsTracking()
     .FirstOrDefaultAsync(p => p.ProductVariants.Any(v => v.Id == variantId));

        return product?.ProductVariants.FirstOrDefault(v => v.Id == variantId);
    }

	public async Task<bool> IsSequenceDuplicateAsync(long productId, int sequence)
	{
        return await Context.Products
            .Where(p => p.Id == productId)
            .SelectMany(p => p.Images)
            .AnyAsync(img => img.Sequence == sequence);

	}
}