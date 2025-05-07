using Common.Application;
using Shop.Domain.ProductAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariantStatusCommand
{
    public class ChangeProductVariantStatusCommandHandler : IBaseCommandHandler<ChangeProductVariantStatusCommand>
    {
        private readonly IProductRepository _productRepository;

        public ChangeProductVariantStatusCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult> Handle(ChangeProductVariantStatusCommand request, CancellationToken cancellationToken)
        {
            var productVariant = await _productRepository.GetVariantById(request.ProductVariantId);

            if (productVariant == null)
                return OperationResult.NotFound();

            productVariant.ChangeVariantStatus(request.Status);

            await _productRepository.Save();
            return OperationResult.Success();
        }
    }
}
