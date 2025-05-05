using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariant.RemoveProductVariant
{
    public class RemoveProductVariantCommand:IBaseCommand
    {
        public long ProductId { get; set; }
        public long ProductVariantId { get; set; }

        public RemoveProductVariantCommand(long productId, long productVariantId)
        {
            ProductId = productId;
            ProductVariantId = productVariantId;
        }
    }
}
