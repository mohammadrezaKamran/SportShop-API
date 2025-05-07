using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
using System.Net.NetworkInformation;

namespace Shop.Query.Products;

public static class ProductMapper
{
    public static ProductDto? Map(this Product? product)
    {
        if (product == null)
            return null;
        return new ProductDto()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Description = product.Description,
            ImageName = product.ImageName,
            Slug = product.Slug,
            Title = product.Title,
            BrandName = product.BrandName,
            SeoData = product.SeoData,
            Status=product.Status,

            ProductVariants = product.ProductVariants?
            .Select(variant => new ProductVariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                SKU = variant.SKU,
                Color = variant.Color,
                Size = variant.Size,
                StockQuantity = variant.StockQuantity,
                Price = variant.Price,
                DiscountPercentage = variant.DiscountPercentage,
                VariantStatus=variant.Status,
            }).ToList() ?? new List<ProductVariantDto>(),

     
            Specifications = product.Specifications.Select(s => new ProductSpecificationDto()
            {
                Value = s.Value,
                Key = s.Key
            }).ToList(),
            Images = product.Images.Select(s => new ProductImageDto()
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                ImageName = s.ImageName,
                ProductId = s.ProductId,
                Sequence = s.Sequence
            }).ToList(),
            Category = new()
            {
                Id = product.CategoryId
            },
            SubCategory = new()
            {
                Id = product.SubCategoryId
            },
            SecondarySubCategory = product.SecondarySubCategoryId != null ? new()
            {
                Id = (long)product.SecondarySubCategoryId
            } : null,
        };
    }
    public static ProductFilterData MapListData(this Product product)
    {
        return new ProductFilterData()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            ImageName = product.ImageName,
            Slug = product.Slug,
            Title = product.Title
        };
    }
    public static async Task SetCategories(this ProductDto product, ShopContext context)
    {
        var categories = await context.Categories
            .Where(r => r.Id == product.Category.Id || r.Id == product.SubCategory.Id)
            .Select(s => new ProductCategoryDto()
            {
                Id = s.Id,
                Slug = s.Slug,
                ParentId = s.ParentId,
                SeoData = s.SeoData,
                Title = s.Title
            }).ToListAsync();

        if (product.SecondarySubCategory != null)
        {
            var secondarySubCategory = await context.Categories
                .Where(f => f.Id == product.SecondarySubCategory.Id)
                .Select(s => new ProductCategoryDto()
                {
                    Id = s.Id,
                    Slug = s.Slug,
                    ParentId = s.ParentId,
                    SeoData = s.SeoData,
                    Title = s.Title
                })
                .FirstOrDefaultAsync();

            if (secondarySubCategory != null)
                product.SecondarySubCategory = secondarySubCategory;
        }
        product.Category = categories.First(r => r.Id == product.Category.Id);
        product.SubCategory = categories.First(r => r.Id == product.SubCategory.Id);
    }
}