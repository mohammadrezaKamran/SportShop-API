﻿using Common.Query;
using Common.Query.Filter;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Products.DTOs;

public class ProductShopResult : BaseFilter<ProductShopDto, ProductShopFilterParam>
{
    public CategoryDto? CategoryDto { get; set; }
}

public class ProductShopDto : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string BrandName { get; set; }
	public string ImageName { get; set; }
	public string AltText { get; set; }
    public bool IsSpecial {  get; set; }
	public List<ProductImageDto> Images { get; set; }
	public ProductStatus Status { get; set; }
	public List<ProductVariantShopDto> ProductVariantsShop { get; set; }

}
public class ProductShopFilterParam : BaseFilterParam
{
    public string? CategorySlug { get; set; } = "";
    public string? Search { get; set; } = "";
    public bool OnlyAvailableProducts { get; set; } = false;
    public bool JustHasDiscount { get; set; } = false;
	public bool SpecialProducts { get; set; } = false;
	public ProductSearchOrderBy SearchOrderBy { get; set; } = ProductSearchOrderBy.Cheapest;
}

public class ProductCategoryResult : BaseFilter<ProductShopDto, ProductCategoryFilterParam>
{
	public CategoryDto? CategoryDto { get; set; }
}

public class ProductCategoryFilterParam : BaseFilterParam
{
    public ProductSearchOrderBy SearchOrderBy { get; set; } = ProductSearchOrderBy.Cheapest;
    public string CategorySlug { get; set; } = "";
    public string? Search { get; set; } = "";
    public bool? IsSpecial { get; set; } = false;
	public List<string>? Colors { get; set; }
	public List<string>? Sizes { get; set; }
	public string? Brand { get; set; } = "";

}

public enum ProductSearchOrderBy
{
    Latest,
    Expensive,
    Cheapest,
}