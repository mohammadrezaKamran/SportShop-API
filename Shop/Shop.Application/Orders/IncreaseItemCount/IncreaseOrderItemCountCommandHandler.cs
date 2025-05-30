using Common.Application;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Orders.IncreaseItemCount
{
    public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;
		public IncreaseOrderItemCountCommandHandler(IOrderRepository repository, IProductRepository productRepository)
		{
			_repository = repository;
			_productRepository = productRepository;
		}

		public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
        {
            var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
            if (currentOrder == null)
                return OperationResult.NotFound();

			var orderItem = currentOrder.Items.FirstOrDefault(i => i.Id == request.ItemId);
			if (orderItem == null)
				return OperationResult.NotFound("آیتم مورد نظر یافت نشد.");

			var product = await _productRepository.GetVariantById(orderItem.ProductVariantId);
			if (product == null)
				return OperationResult.NotFound("محصول یافت نشد.");

			try
			{
				currentOrder.IncreaseItemCount(request.ItemId, request.Count, product.StockQuantity);
			}
			catch (InvalidOperationException ex)
			{
				return OperationResult.Error(ex.Message);
			}

			await _repository.Save();
            return OperationResult.Success();
        }
    }
}