using Common.Application;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariant.EditProductVariant
{
    public class EditProductVariantCommandHandler : IBaseCommandHandler<EditProductVariantCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _domainService;

        public EditProductVariantCommandHandler(IProductRepository productRepository, IProductDomainService productDomainService)
        {
            _repository = productRepository;
            _domainService = productDomainService;
        }

        public async Task<OperationResult> Handle(EditProductVariantCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetTracking(request.ProductId);
            if (product == null)
                return OperationResult.NotFound();

            product.EditVariant(request.ProductVariantId, request.Color, request.Size, request.StockQuantity, request.Price,
                request.DiscountPercentage, request.SKU, _domainService);

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
