using Common.Application;
using Shop.Application.Products.ProductVariant.RemoveProductVariant;
using Shop.Domain.ProductAgg.Repository;

public class RemoveProductVariantCommandHandler : IBaseCommandHandler<RemoveProductVariantCommand>
{
    private readonly IProductRepository _repository;

    public RemoveProductVariantCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveProductVariantCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        product.RemoveVariant(request.ProductVariantId);

        await _repository.Save();
        return OperationResult.Success();
    }
}