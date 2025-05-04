using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariant.AddProductVariant
{
    public class AddProductVariant:IBaseCommand
    {
        public long ProductId { get; private set; }
        public string? Color { get; private set; }
        public int? Weight { get; private set; }
        public int StockQuantity { get; private set; }
        public decimal Price { get; private set; }
        public int? DiscountPercentage { get; private set; }
    }
}
