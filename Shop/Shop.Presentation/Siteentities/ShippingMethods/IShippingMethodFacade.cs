using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.SiteEntities.ShippingMethods.Create;
using Shop.Application.SiteEntities.ShippingMethods.Delete;
using Shop.Application.SiteEntities.ShippingMethods.Edit;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.ShippingMethods.GetById;
using Shop.Query.SiteEntities.ShippingMethods.GetList;

namespace Shop.Presentation.Facade.Siteentities.ShippingMethods;

public interface IShippingMethodFacade
{
    Task<OperationResult> Create(CreateShippingMethodCommand command);
    Task<OperationResult> Edit(EditShippingMethodCommand command);
    Task<OperationResult> Delete(long id);


    Task<ShippingMethodDto?> GetShippingMethodById(long id);
    Task<List<ShippingMethodDto>> GetList();
}

internal class ShippingMethodFacade : IShippingMethodFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<ShippingMethodFacade> _logger;
	public ShippingMethodFacade(IMediator mediator, ILogger<ShippingMethodFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> Create(CreateShippingMethodCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while creating shipping method");
			return OperationResult.Error("خطا در ایجاد روش ارسال");
		}
	}

	public async Task<OperationResult> Edit(EditShippingMethodCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while editing shipping method");
			return OperationResult.Error("خطا در ویرایش روش ارسال");
		}
	}

	public async Task<OperationResult> Delete(long id)
	{
		try
		{
			return await _mediator.Send(new DeleteShippingMethodCommand(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while deleting shipping method with id {Id}", id);
			return OperationResult.Error("خطا در حذف روش ارسال");
		}
	}

	public async Task<ShippingMethodDto?> GetShippingMethodById(long id)
	{
		try
		{
			return await _mediator.Send(new GetShippingMethodByIdQuery(id));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while getting shipping method with id {Id}", id);
			return null;
		}
	}

	public async Task<List<ShippingMethodDto>> GetList()
	{
		try
		{
			return await _mediator.Send(new GetShippingMethodsByListQuery());
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error while getting shipping methods list");
			return new List<ShippingMethodDto>();
		}
	}
}