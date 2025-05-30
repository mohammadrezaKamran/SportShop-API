using Common.Application;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg;

namespace Shop.Application.Orders.DecreaseItemCount;

public class DecreaseOrderItemCountCommandHandler : IBaseCommandHandler<DecreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _repository;

    public DecreaseOrderItemCountCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DecreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();
      
		try
		{
			currentOrder.DecreaseItemCount(request.ItemId, request.Count);
		}
		catch (InvalidOperationException ex)
		{
			return OperationResult.Error(ex.Message);
		}

		await _repository.Save();
        return OperationResult.Success();
    }
}