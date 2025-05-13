using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products;

public class ProductDomainService:IProductDomainService
{
    private readonly IProductRepository _repository;

    public ProductDomainService(IProductRepository repository)
    {
        _repository = repository;
    }

    public bool SKUIsExist(string sku, long productId)
    {
        return _repository.Exists(product =>
             product.ProductVariants.Any(v => v.SKU == sku) &&
             product.Id != productId);
    }

    public bool SlugIsExist(string slug)
    {
        return _repository.Exists(s => s.Slug == slug);
    }
}