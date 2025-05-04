using Common.Application;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariant.AddProductVariant
{
    public class AddProductVariantHandler : IBaseCommandHandler<AddProductVariant>
    {
        private readonly IProductRepository _repository;

        public AddProductVariantHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<OperationResult> Handle(AddProductVariant request, CancellationToken cancellationToken)
        {
            var product=_repository.Get(request.ProductId);
            if (product == null)
                return null;
            product.AddVariant(new Domain.ProductAgg.ProductVariant() { });
        }
    }
}
