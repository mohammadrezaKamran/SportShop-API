using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariantStatusCommand
{
    public class ChangeProductVariantStatusCommand : IBaseCommand
    {
        public long ProductVariantId { get; set; }
        public ProductVariantStatus Status { get; set; }
    }
}
