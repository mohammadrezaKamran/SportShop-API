using Common.Query;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetProductVariantById
{
    public record GetProductVariantByIdQuery(long ProductVariantId) : IQuery<ProductVariantDto?>;
}
