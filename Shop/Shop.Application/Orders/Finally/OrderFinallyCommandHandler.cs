using Common.Application;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Orders.Finally;

public class OrderFinallyCommandHandler : IBaseCommandHandler<OrderFinallyCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    public OrderFinallyCommandHandler(IOrderRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(OrderFinallyCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetTracking(request.OrderId);
        if (order == null)
            return OperationResult.NotFound();

        foreach (var item in order.Items)
        {
            var variant = await _productRepository.GetVariantById(item.ProductVariantId);
            if (variant == null)
                return OperationResult.NotFound();
			try
			{
				variant.DecreaseStock(item.Count);
			}
			catch (InvalidOperationException ex)
			{
				return OperationResult.Error(ex.Message);
			}
		}
        order.Finally(request.TextForInvoice);
        await _repository.Save();
        return OperationResult.Success();
    }
}