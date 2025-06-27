using Common.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Categories;
using Shop.Query.Categories.DTOs;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetProductForCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

internal class GetProductForCategoryQueryHandler : IQueryHandler<GetProductForCategoryQuery, ProductCategoryResult>
{
    private readonly ShopContext _context;

    public GetProductForCategoryQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductCategoryResult> Handle(GetProductForCategoryQuery request, CancellationToken cancellationToken)
    {
		var @params = request.FilterParams;
		if (string.IsNullOrWhiteSpace(@params.CategorySlug))
			return new ProductCategoryResult { Data = new List<ProductShopDto>(), FilterParams = @params };

		var category = await _context.Categories
			.AsNoTracking()
			.FirstOrDefaultAsync(f => f.Slug == @params.CategorySlug, cancellationToken);

		if (category == null)
			return new ProductCategoryResult { Data = new List<ProductShopDto>(), FilterParams = @params };

		var query = _context.Products
			.AsNoTracking()
			.Include(p => p.ProductVariants)
			.Include(p => p.Images)
			.Where(p =>
				p.CategoryId == category.Id ||
				p.SubCategoryId == category.Id ||
				p.SecondarySubCategoryId == category.Id
			);

		
		// فیلتر جستجو
		if (!string.IsNullOrWhiteSpace(@params.Search))
		{
			var search = @params.Search.Trim().ToLower();
			query = query.Where(p => p.Title.ToLower().Contains(search));
		}

		// فیلتر برند
		if (@params.Brand != null && @params.Brand.Any())
		{
			query = query.Where(p => @params.Brand.Contains(p.BrandName));
		}

		// فیلتر رنگ و سایز روی واریانت‌ها
		if ((@params.Colors != null && @params.Colors.Any()) || (@params.Sizes != null && @params.Sizes.Any()))
		{
			query = query.Where(p => p.ProductVariants.Any(v =>
				(@params.Colors == null || @params.Colors.Count == 0 || @params.Colors.Contains(v.Color)) &&
				(@params.Sizes == null || @params.Sizes.Count == 0 || @params.Sizes.Contains(v.Size))
			));
		}
		query = query.OrderByDescending(p =>
			p.ProductVariants.Any(v =>
			v.Status != ProductVariantStatus.OutOfStock
	));
		// مرتب‌سازی
		query = @params.SearchOrderBy switch
		{
			ProductSearchOrderBy.Cheapest => query.OrderBy(p => p.ProductVariants
												  .Where(d => d.Status != ProductVariantStatus.OutOfStock)
											   	  .Min(d => (decimal?)d.Price) ?? decimal.MaxValue)
			,
			ProductSearchOrderBy.Expensive => query.OrderBy(p => p.ProductVariants
                                                   .Where(d => d.Status != ProductVariantStatus.OutOfStock)
                                                   .Max(d => (decimal?)d.Price) ?? decimal.MaxValue),

			ProductSearchOrderBy.Latest => query.OrderByDescending(p => p.Id),
			_ => query.OrderBy(p => p.Id)
		};

		var totalCount = await query.CountAsync(cancellationToken);

		var skip = Math.Max(@params.PageId, 1) - 1;
		var products = await query
			.Skip(skip * @params.Take)
			.Take(@params.Take)
			.Select(p => new ProductShopDto
			{
				Id = p.Id,
				CreationDate = p.CreationDate,
				Title = p.Title,
				ImageName = p.ImageName,
				BrandName = p.BrandName,
				Slug = p.Slug,
				Status = p.Status,
				IsSpecial = p.IsSpecial,
				AltText = p.AltText,

				Images = p.Images.Select(s => new ProductImageDto
				{
					Id = s.Id,
					CreationDate = s.CreationDate,
					ImageName = s.ImageName,
					ProductId = s.ProductId,
					Sequence = s.Sequence,
					AltText=s.AltText

				}).ToList(),
				ProductVariantsShop = p.ProductVariants.Select(variant => new ProductVariantShopDto
				{
					Id = variant.Id,
					CreationDate = variant.CreationDate,
					ProductId = variant.ProductId,
					SKU = variant.SKU,
					Color = variant.Color,
					Size = variant.Size,
					StockQuantity = variant.StockQuantity,
					Price = variant.Price,
					DiscountPercentage = variant.DiscountPercentage,
					VariantStatus = variant.Status
				}).ToList()
			})
			.ToListAsync(cancellationToken);

		var model = new ProductCategoryResult
		{
			FilterParams = @params,
			Data = products,
			CategoryDto = category.Map(),			
		};
		model.GeneratePaging(totalCount, @params.Take, @params.PageId);

		return model;
	}
}
