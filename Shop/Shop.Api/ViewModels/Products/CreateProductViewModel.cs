using Common.Domain.ValueObjects;
using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace Shop.Api.ViewModels.Products;

public class CreateProductViewModel
{
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public long SecondarySubCategoryId { get; set; }
    public string Slug { get; set; }
    public string BrandName {  get; set; }
    public string ColorHex { get; set; }
    public SeoDataViewModel SeoData { get; set; }
    public string Specifications { get; set; }

    public Dictionary<string, string> GetSpecification()
    {
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(Specifications);
    }
}
public class EditProductViewModel
{
    public long ProductId { get; set; }
    public string Title { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public long SecondarySubCategoryId { get; set; }
    public string Slug { get; set; }
    public string ColorHex {  get; set; }
    public string BrandName {  get; set; }
    public ProductStatus Status { get; set; }
    public SeoDataViewModel SeoData { get; set; }
    public string Specifications { get; set; }

    public Dictionary<string, string> GetSpecification()
    {
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(Specifications);
    }
}
public class SeoDataViewModel
{
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeyWords { get; set; }
    public bool IndexPage { get; set; }
    public string? Canonical { get; set; }
    public string? Schema { get; set; }

    public SeoData Map()
    {
        return new SeoData(MetaKeyWords, MetaDescription, MetaTitle, IndexPage, Canonical, Schema);
    }
}
public class ProductInventoryViewModel
{
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public int? DiscountPercentage { get; set; }

}
public class AddInventoryViewModel
{
    public long ProductId { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public int? PercentageDiscount { get; set; }
}
public class EditInventoryViewModel
{
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public int? PercentageDiscount { get; private set; }
}