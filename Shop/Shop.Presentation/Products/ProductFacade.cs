using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.ProductVariant.AddProductVariant;
using Shop.Application.Products.ProductVariant.EditProductVariant;
using Shop.Application.Products.ProductVariant.RemoveProductVariant;
using Shop.Application.Products.ProductVariantStatusCommand;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetProductForShop;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;

    public ProductFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateProduct(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditProduct(EditProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddImage(AddProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);

        }
        return result;
    }

    public async Task<OperationResult> RemoveImage(RemoveProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
        }
        return result;
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
     
            return await _mediator.Send(new GetProductBySlugQuery(slug));
     
    }

    public async Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductsByFilterQuery(filterParams));
    }

    public async Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams)
    {
        return await _mediator.Send(new GetProductForShopQuery(filterParams));
    }

    public async Task<OperationResult> AddProductVariant(AddProductVariantCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditProductVariant(EditProductVariantCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveProductVariant(RemoveProductVariantCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangeProductVariantStatus(ChangeProductVariantStatusCommand command)
    {
        return await _mediator.Send(command);
    }
}