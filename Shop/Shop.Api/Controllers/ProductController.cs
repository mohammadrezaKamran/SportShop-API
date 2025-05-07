using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Products;
using Shop.Application.Orders.Status;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.ProductVariant.AddProductVariant;
using Shop.Application.Products.ProductVariant.EditProductVariant;
using Shop.Application.Products.ProductVariant.RemoveProductVariant;
using Shop.Application.Products.ProductVariantStatusCommand;
using Shop.Application.Products.RemoveImage;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;
using System.Drawing;

namespace Shop.Api.Controllers;

//[PermissionChecker(Permission.CRUD_Product)]
public class ProductController : ApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetProductByFilter([FromQuery] ProductFilterParams filterParams)
    {
        return QueryResult(await _productFacade.GetProductsByFilter(filterParams));
    }
    [AllowAnonymous]
    [HttpGet("Shop")]
    public async Task<ApiResult<ProductShopResult>> GetProductForShopFilter([FromQuery] ProductShopFilterParam filterParams)
    {
        return QueryResult(await _productFacade.GetProductForShop(filterParams));
    }

    [AllowAnonymous]
    [HttpGet("{productId}")]
    public async Task<ApiResult<ProductDto?>> GetProductById(long productId)
    {
        var product = await _productFacade.GetProductById(productId);
        return QueryResult(product);
    }

    [AllowAnonymous]
    [HttpGet("bySlug/{slug}")]
    public async Task<ApiResult<ProductDto?>> GetProductBySlug(string slug)
    {
        var product = await _productFacade.GetProductBySlug(slug);
        return QueryResult(product);
    }

    [HttpPost]
    public async Task<ApiResult> CreateProduct([FromForm] CreateProductViewModel command)
    {
        var result = await _productFacade.CreateProduct(new CreateProductCommand()
        {
            SeoData = command.SeoData.Map(),
            CategoryId = command.CategoryId,
            Description = command.Description,
            ImageFile = command.ImageFile,
            SecondarySubCategoryId = command.SecondarySubCategoryId,
            Slug = command.Slug,
            BrandName = command.BrandName,
            Specifications = command.GetSpecification(),
            SubCategoryId = command.SubCategoryId,
            Title = command.Title,
            Status = command.Status,
        });
        return CommandResult(result);
    }

    [HttpPost("ProductVariant")]
    public async Task<ApiResult> AddProductVariant(AddProductVariantViewModel command)
    {
        var result = await _productFacade.AddProductVariant(new AddProductVariantCommand()
        {
            ProductId = command.ProductId,
            StockQuantity = command.StockQuantity,
            Price = command.Price,
            DiscountPercentage = command.DiscountPercentage,
            SKU = command.SKU,
            Size = command.Size,
            Color = command.Color
        });

        return CommandResult(result);
    }
    [HttpPut("ProductVariant")]
    public async Task<ApiResult> EditProductVariant(EditProductVariantViewModel command)
    {
        var result = await _productFacade.EditProductVariant(new EditProductVariantCommand()
        {
            ProductId = command.ProductId,
            ProductVariantId = command.ProductVariantId,
            StockQuantity = command.StockQuantity,
            Price = command.Price,
            DiscountPercentage = command.DiscountPercentage,
            SKU = command.SKU,
            Size = command.Size,
            Color = command.Color
        });

        return CommandResult(result);
    }

    [HttpDelete("ProductVariant")]
    public async Task<ApiResult> RemoveProductVariant(RemoveProductVariantCommand command)
    {
        var result = await _productFacade.RemoveProductVariant(command);
        return CommandResult(result);
    }

    [HttpPost("ChangeStatus")]
    public async Task<ApiResult> ChangeProductVariantStatus(ChangeProductVariantStatusCommand command)
    {
        var result = await _productFacade.ChangeProductVariantStatus(command);
        return CommandResult(result);
    }

    [HttpPost("images")]
    public async Task<ApiResult> AddImage([FromForm] AddProductImageCommand command)
    {
        var result = await _productFacade.AddImage(command);
        return CommandResult(result);
    }

    [HttpDelete("images")]
    public async Task<ApiResult> RemoveImage(RemoveProductImageCommand command)
    {
        var result = await _productFacade.RemoveImage(command);
        return CommandResult(result);
    }
    [HttpPut]
    public async Task<ApiResult> EditProduct([FromForm] EditProductViewModel command)
    {
        var result = await _productFacade.EditProduct(new EditProductCommand(command.ProductId, command.Title, command.ImageFile,
            command.Description, command.CategoryId, command.SubCategoryId, command.Status, command.SecondarySubCategoryId, command.Slug, command.SeoData.Map(),
            command.BrandName, command.GetSpecification()));

        return CommandResult(result);
    }
}