using Common.Domain.ValueObjects;
using Common.Query;
using Shop.Domain.ProductAgg;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Products.DTOs;

public class ProductDto : BaseDto
{
    public string Title { get; set; }
    public string ImageName { get; set; }
	public string AltText { get; set; }
	public string Description { get; set; }
    public string BrandName { get; set; }
    public bool IsSpecial {  get; set; }
    public ProductCategoryDto Category { get; set; }
    public ProductCategoryDto SubCategory { get; set; }
    public ProductCategoryDto? SecondarySubCategory { get; set; }
    public List<ProductVariantDto> ProductVariants { get; set; }
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


public class ProductVariantDto:BaseDto
{
    public long ProductId { get; set; }
    public string SKU { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public int? DiscountPercentage { get; set; }
    public ProductVariantStatus VariantStatus { get; set; }
}

public class ProductVariantShopDto : BaseDto
{
	public long ProductId { get; set; }
	public string SKU { get; set; }
	public string? Color { get; set; }
	public string? Size { get; set; }
	public int StockQuantity { get; set; }
	public decimal Price { get; set; }
	public int? DiscountPercentage { get; set; }
	public ProductVariantStatus VariantStatus { get; set; }

	public decimal TotalPrice =>
	  DiscountPercentage.HasValue
	  ? Price - (Price * DiscountPercentage.Value / 100)
	  : Price;
}
