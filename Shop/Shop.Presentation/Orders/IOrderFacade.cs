﻿using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.Finally;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetTrackingNumber;
using Shop.Application.Orders.Status;
using Shop.Query.Orders.DTOs;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> AddOrderItem(AddOrderItemCommand command);
    Task<OperationResult> OrderCheckOut(CheckoutOrderCommand command);
    Task<OperationResult> RemoveOrderItem(RemoveOrderItemCommand command);
    Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command);
    Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command);
    Task<OperationResult> FinallyOrder(OrderFinallyCommand command);
    Task<OperationResult> ChangeOrderStatus(ChangeOrderStatusCommand command);
    Task<OperationResult> SendOrder(long orderId);
	Task<OperationResult> SetTrackingNumber(SetTrackingNumberCommand command);


	Task<OrderDto?> GetOrderById(long orderId);
    Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams);
    Task<OrderDto?> GetCurrentOrder(long userId);
	Task<OrderDto?> GetCurrentHistoryOrder(long userId);
	Task<int?> GetNumberOfItems(long userId);
}