using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;

    public AddOrderItemCommandHandler(IOrderRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }
    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var variant = await _productRepository.GetVariantById(request.ProductVariantId);
        if (variant == null)
            return OperationResult.NotFound();

        if (variant.StockQuantity < request.Count)
            return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

        var order = await _repository.GetCurrentUserOrder(request.UserId);
        if (order == null)
        {
            order = new Order(request.UserId);
            order.AddItem(new OrderItem(request.ProductVariantId, request.Count, variant.Price));
            _repository.Add(order);
        }
        else
        {
            order.AddItem(new OrderItem(request.ProductVariantId, request.Count, variant.Price));
        }

        if (ItemCountBeggerThanInventoryCount(variant, order))
            return OperationResult.Error("تعداد محصولات موجود کمتر از حد درخواستی است.");

        await _repository.Save();
        return OperationResult.Success();
    }

    private bool ItemCountBeggerThanInventoryCount(ProductVariant variant, Order order)
    {
        var orderItem = order.Items.First(f => f.ProductVariantId == variant.Id);
        return orderItem.Count > variant.StockQuantity;
    }
}
