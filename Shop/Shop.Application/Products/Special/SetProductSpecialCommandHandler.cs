using Common.Application;
using Shop.Application.Products.Special;
using Shop.Domain.ProductAgg.Repository;

public class SetProductSpecialCommandHandler : IBaseCommandHandler<SetProductSpecialCommand>
{
	private readonly IProductRepository _productRepository;

	public SetProductSpecialCommandHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<OperationResult> Handle(SetProductSpecialCommand request, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetAsync(request.ProductId);

		if (product == null)
			return OperationResult.NotFound();

		if (request.IsSpecial)
			product.MarkAsSpecial();
		else
			product.UnmarkAsSpecial();

		await _productRepository.Save();

		return OperationResult.Success() ;
	}
}
