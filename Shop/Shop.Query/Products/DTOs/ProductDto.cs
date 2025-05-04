using Common.Domain.ValueObjects;
using Common.Query;
using Shop.Domain.ProductAgg;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Products.DTOs;

public class ProductDto : BaseDto
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
    public string BrandName { get; set; }
    public ProductCategoryDto Category { get; set; }
    public ProductCategoryDto SubCategory { get; set; }
    public ProductCategoryDto? SecondarySubCategory { get; set; }
   public List<ProductInventoryDto> Inventories { get; set; }
    public ProductStatus Status { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public List<ProductImageDto> Images { get; set; }
    public List<ProductSpecificationDto> Specifications { get; set; }
}

public class ProductCategoryDto
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
}


public class ProductInventoryDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public bool IsAvailable { get; set; } 
    public List<ProductInventoryItemDto> InventoryItems { get; set; } 
}
public class ProductInventoryItemDto
{
    public long Id { get; set; }
    public long InventoryId { get; set; }
    public string? Color { get; set; } 
    public int? Weight { get; set; } 
    public int StockQuantity { get; set; } 
    public decimal Price { get; set; } 
    public int? DiscountPercentage { get; set; }
}
