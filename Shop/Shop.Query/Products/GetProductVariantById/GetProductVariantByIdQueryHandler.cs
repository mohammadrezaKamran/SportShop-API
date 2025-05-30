using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetProductVariantById;

public class GetProductVariantByIdQueryHandler : IQueryHandler<GetProductVariantByIdQuery, ProductVariantDto?>
{
    private readonly ShopContext _context;

    public GetProductVariantByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductVariantDto?> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking().Where(s => s.ProductVariants.Any(v => v.Id == request.ProductVariantId))
            .Select(v => v.ProductVariants.Where(u => u.Id == request.ProductVariantId)
            .Select(u => new ProductVariantDto
            {
                Id = u.Id,
                CreationDate = u.CreationDate,
                ProductId = u.ProductId,
                SKU = u.SKU,
                Color = u.Color,
                Size = u.Size,
                StockQuantity = u.StockQuantity,
                Price = u.Price,
                DiscountPercentage = u.DiscountPercentage,
                VariantStatus = u.Status,
            }).FirstOrDefault()).FirstOrDefaultAsync();

        return product;

    }
}
