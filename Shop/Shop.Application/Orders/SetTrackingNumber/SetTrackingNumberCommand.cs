using Common.Application;
using Shop.Application.Orders.SetTrackingNumber;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.SetTrackingNumber
{
	public class SetTrackingNumberCommand:IBaseCommand
	{
		public long OrderId { get; set; }
		public string TrackingNumber {  get; set; }
	}
}
public class SetTrackingNumberCommandHandler : IBaseCommandHandler<SetTrackingNumberCommand>
{
	private readonly IOrderRepository _orderRepository;

	public SetTrackingNumberCommandHandler(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	public async Task<OperationResult> Handle(SetTrackingNumberCommand request, CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetTracking(request.OrderId);
		if (order == null)
			return OperationResult.NotFound();

		try
		{
			order.SetTrackingNumber(request.TrackingNumber);
		}
		catch (InvalidOperationException ex)
		{
			return OperationResult.Error(ex.Message);
		}
		
		await _orderRepository.Save();
		return OperationResult.Success();
	}
}