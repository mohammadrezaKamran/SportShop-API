using Common.Application;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.ProductVariant.AddProductVariant;
using Shop.Application.Products.ProductVariant.EditProductVariant;
using Shop.Application.Products.ProductVariant.RemoveProductVariant;
using Shop.Application.Products.ProductVariantStatusCommand;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult> CreateProduct(CreateProductCommand command);
    Task<OperationResult> EditProduct(EditProductCommand command);
    Task<OperationResult> AddImage(AddProductImageCommand command);
    Task<OperationResult> RemoveImage(RemoveProductImageCommand command);
    Task<OperationResult> AddProductVariant(AddProductVariantCommand command);
    Task<OperationResult> EditProductVariant(EditProductVariantCommand command);
    Task<OperationResult> RemoveProductVariant(RemoveProductVariantCommand command);
    Task<OperationResult> ChangeProductVariantStatus(ChangeProductVariantStatusCommand command);

    Task<ProductDto?> GetProductById(long productId);
    Task<ProductDto?> GetProductBySlug(string slug);
    Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams);
    Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams);
}
