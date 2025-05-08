using Common.Application;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariant.AddProductVariant
{
    public class AddProductVariantCommandHandler : IBaseCommandHandler<AddProductVariantCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _domainService;

        public AddProductVariantCommandHandler(IProductRepository repository, IProductDomainService domainService)
        {
            _repository = repository;
            _domainService = domainService;
        }

        public async Task<OperationResult> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetTracking(request.ProductId);
            if (product == null)
                return OperationResult.NotFound();

            var variant = new Domain.ProductAgg.ProductVariant(
            request.ProductId,
            request.Color,
            request.Size,
            request.StockQuantity,
            request.Price,
            request.DiscountPercentage,
            request.SKU,
            _domainService);

            product.AddVariant(variant);

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
