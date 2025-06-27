using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Categories;
using Shop.Query.Categories.DTOs;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetProductForShop
{
    internal class GetProductForShopQueryHandler : IQueryHandler<GetProductForShopQuery, ProductShopResult>
    {
        private readonly ShopContext _context;

        public GetProductForShopQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<ProductShopResult> Handle(GetProductForShopQuery request, CancellationToken cancellationToken)
        {

            var @params = request.FilterParams;
            CategoryDto? selectedCategory = null;

            var query = _context.Products
                .Include(p => p.ProductVariants)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.CategorySlug))
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(f => f.Slug == @params.CategorySlug, cancellationToken);

                if (category != null)
                {
                    query = query.Where(p =>
                        p.CategoryId == category.Id ||
                        p.SubCategoryId == category.Id ||
                        p.SecondarySubCategoryId == category.Id
                    );
                    selectedCategory = category.Map();
                }
            }

            // **🔹 فیلتر جستجو در عنوان**
            if (!string.IsNullOrWhiteSpace(@params.Search))
            {
                query = query.Where(p => p.Title.Contains(@params.Search));
            }

            // فقط محصولات موجود
            if (@params.OnlyAvailableProducts)
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.StockQuantity > 0));
            }

			// فقط محصولات ویژه
			if (@params.SpecialProducts)
			{
				query = query.Where(p => p.IsSpecial==true);
			}

			//  فقط محصولات دارای تخفیف
			if (@params.JustHasDiscount)
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.DiscountPercentage > 0));
            }

            // مرتب‌سازی بر اساس فیلتر کاربر
            query = @params.SearchOrderBy switch
            {
                ProductSearchOrderBy.Cheapest => query.OrderBy(p => p.ProductVariants.Min(d => d.Price)),
                ProductSearchOrderBy.Expensive => query.OrderByDescending(p => p.ProductVariants.Max(d => d.Price)),
                ProductSearchOrderBy.Latest => query.OrderByDescending(p => p.Id),
                _ => query.OrderBy(p => p.Id)
            };

            // محاسبه تعداد کل نتایج
            var totalCount = await query.CountAsync(cancellationToken);

            // صفحه‌بندی
            var skip = (@params.PageId - 1) * @params.Take;
            var products = await query
                .Skip(skip)
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

					Images = p.Images.Select(s => new ProductImageDto()
					{
						Id = s.Id,
						CreationDate = s.CreationDate,
						ImageName = s.ImageName,
						ProductId = s.ProductId,
						Sequence = s.Sequence,
                        AltText=s.AltText,
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
                        VariantStatus=variant.Status
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            // آماده‌سازی نتیجه
            var model = new ProductShopResult
            {
                FilterParams = @params,
                Data = products,
                CategoryDto = selectedCategory
            };
            model.GeneratePaging(totalCount, @params.Take, @params.PageId);

            return model;
        }
    }

}

