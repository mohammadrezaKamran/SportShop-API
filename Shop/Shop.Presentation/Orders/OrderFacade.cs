using Common.Application;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.Finally;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SendOrder;
using Shop.Application.Orders.SetTrackingNumber;
using Shop.Application.Orders.Status;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;
using Shop.Query.Orders.GetNumberOfItems;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Orders;

internal class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;
	private readonly ILogger<OrderFacade> _logger;
	public OrderFacade(IMediator mediator, ILogger<OrderFacade> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task<OperationResult> AddOrderItem(AddOrderItemCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در افزودن آیتم به سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در افزودن آیتم رخ داد.");
		}
	}

	public async Task<OperationResult> OrderCheckOut(CheckoutOrderCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تسویه سفارش: {@Command}", command);
			return OperationResult.Error("خطا در تسویه سفارش رخ داد.");
		}
	}

	public async Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در حذف آیتم از سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در حذف آیتم رخ داد.");
		}
	}

	public async Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در افزایش تعداد آیتم سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در افزایش تعداد آیتم رخ داد.");
		}
	}

	public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در کاهش تعداد آیتم سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در کاهش تعداد آیتم رخ داد.");
		}
	}

	public async Task<OperationResult> FinallyOrder(OrderFinallyCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در نهایی‌سازی سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در نهایی‌سازی سفارش رخ داد.");
		}
	}

	public async Task<OperationResult> SendOrder(long orderId)
	{
		try
		{
			return await _mediator.Send(new SendOrderCommand(orderId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ارسال سفارش با شناسه {OrderId}", orderId);
			return OperationResult.Error("خطایی در ارسال سفارش رخ داد.");
		}
	}

	public async Task<OrderDto?> GetOrderById(long orderId)
	{
		try
		{
			return await _mediator.Send(new GetOrderByIdQuery(orderId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت سفارش با شناسه {OrderId}", orderId);
			return null;
		}
	}

	public async Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams)
	{
		try
		{
			return await _mediator.Send(new GetOrdersByFilterQuery(filterParams));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت لیست سفارش‌ها با فیلتر: {@Filter}", filterParams);
			return new OrderFilterResult();
		}
	}

	public async Task<OrderDto?> GetCurrentOrder(long userId)
	{
		try
		{
			return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت سفارش جاری برای کاربر {UserId}", userId);
			return null;
		}
	}

	public async Task<OperationResult> ChangeOrderStatus(ChangeOrderStatusCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در تغییر وضعیت سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در تغییر وضعیت سفارش رخ داد.");
		}
	}

	public async Task<OperationResult> SetTrackingNumber(SetTrackingNumberCommand command)
	{
		try
		{
			return await _mediator.Send(command);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در ثبت شناسه پیگیری سفارش: {@Command}", command);
			return OperationResult.Error("خطایی در ثبت شناسه پیگیری رخ داد.");
		}
	}

	public async Task<int?> GetNumberOfItems(long userId)
	{
		try
		{
			return await _mediator.Send(new NOFItemsQuery(userId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت تعداد آیتم‌های سفارش برای کاربر {UserId}", userId);
			return null;
		}
	}

	public async Task<OrderDto?> GetCurrentHistoryOrder(long userId)
	{
		try
		{
			return await _mediator.Send(new GetCurrentUserHistoryOrderQuery(userId));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "خطا در دریافت تاریخچه سفارش جاری برای کاربر {UserId}", userId);
			return null;
		}
	}
}