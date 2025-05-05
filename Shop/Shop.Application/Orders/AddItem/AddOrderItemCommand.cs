using Common.Application;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommand : IBaseCommand
{
    public AddOrderItemCommand(long productVariantId, int count, long userId)
    {
        ProductVariantId = productVariantId;
        Count = count;
        UserId = userId;
    }

    public long ProductVariantId { get; private set; }
    public int Count { get; private set; }
    public long UserId { get; private set; }
}