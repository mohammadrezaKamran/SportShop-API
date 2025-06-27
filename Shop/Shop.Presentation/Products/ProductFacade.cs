using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.ProductVariant.AddProductVariant;
using Shop.Application.Products.ProductVariant.EditProductVariant;
using Shop.Application.Products.ProductVariant.RemoveProductVariant;
using Shop.Application.Products.ProductVariantStatusCommand;
using Shop.Application.Products.RemoveImage;
using Shop.Application.Products.Special;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetFilterDetail;
using Shop.Query.Products.GetProductForCategory;
using Shop.Query.Products.GetProductForShop;
using Shop.Query.Products.GetProductVariantById;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<ProductFacade> _logger;

	public ProductFacade(IMediator mediator, ILogger<ProductFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> CreateProduct(CreateProductCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ایجاد محصول: {@Command}", command);
			return OperationResult.Error("خطا در ایجاد محصول");
		}
	}

	public async Task<OperationResult> EditProduct(EditProductCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش محصول: {@Command}", command);
			return OperationResult.Error("خطا در ویرایش محصول");
		}
	}

	public async Task<OperationResult> AddImage(AddProductImageCommand command)
	{
		try
		{
			return await _mediator.Send(command);

		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در افزودن تصویر محصول: {@Command}", command);
			return OperationResult.Error("خطا در افزودن تصویر");
		}
	}

	public async Task<OperationResult> RemoveImage(RemoveProductImageCommand command)
	{
		try
		{
			return await _mediator.Send(command);

		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف تصویر محصول: {@Command}", command);
			return OperationResult.Error("خطا در حذف تصویر");
		}
	}

	public async Task<ProductDto?> GetProductById(long productId)
	{
		try
		{
			return await _mediator.Send(new GetProductByIdQuery(productId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت محصول با شناسه {Id}", productId);
			return null;
		}
	}

	public async Task<ProductDto?> GetProductBySlug(string slug)
	{
		try
		{
			return await _mediator.Send(new GetProductBySlugQuery(slug));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت محصول با Slug: {Slug}", slug);
			return null;
		}
	}

	public async Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams)
	{
		try
		{
			return await _mediator.Send(new GetProductsByFilterQuery(filterParams));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت لیست محصولات با فیلتر: {@Filter}", filterParams);
			return new ProductFilterResult();
		}
	}

	public async Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams)
	{
		try
		{
			return await _mediator.Send(new GetProductForShopQuery(filterParams));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت محصولات برای فروشگاه: {@Filter}", filterParams);
			return new ProductShopResult();
		}
	}

	public async Task<OperationResult> AddProductVariant(AddProductVariantCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در افزودن واریانت محصول: {@Command}", command);
			return OperationResult.Error("خطا در افزودن واریانت محصول");
		}
	}

	public async Task<OperationResult> EditProductVariant(EditProductVariantCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ویرایش واریانت محصول: {@Command}", command);
			return OperationResult.Error("خطا در ویرایش واریانت محصول");
		}
	}

	public async Task<OperationResult> RemoveProductVariant(RemoveProductVariantCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف واریانت محصول: {@Command}", command);
			return OperationResult.Error("خطا در حذف واریانت محصول");
		}
	}

	public async Task<OperationResult> ChangeProductVariantStatus(ChangeProductVariantStatusCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تغییر وضعیت واریانت محصول: {@Command}", command);
			return OperationResult.Error("خطا در تغییر وضعیت واریانت محصول");
		}
	}

	public async Task<ProductVariantDto?> GetProductVariantById(long productVariantId)
	{
		try
		{
			return await _mediator.Send(new GetProductVariantByIdQuery(productVariantId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت واریانت محصول با شناسه {Id}", productVariantId);
			return null;
		}
	}

	public async Task<OperationResult> SetProductSpecial(SetProductSpecialCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تعیین وضعیت ویژه برای محصول: {@Command}", command);
			return OperationResult.Error("خطا در تعیین محصول ویژه");
		}
	}

    public async Task<ProductCategoryResult> GetProductsForCategory(ProductCategoryFilterParam filterParams)
    {
        try
        {
            return await _mediator.Send(new GetProductForCategoryQuery(filterParams));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت محصولات برای فروشگاه: {@Filter}", filterParams);
            return new ProductCategoryResult();
        }
    }

	public async Task<FilterDetailsDto> GetFilterDetails()
	{
		try
		{
			return await _mediator.Send(new GetFilterDetailQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت جزییات فیلتر ها برای فروشگاه");
			return new FilterDetailsDto();
		}
	}
}