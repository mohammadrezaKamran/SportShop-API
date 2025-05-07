using Common.Application;
using Shop.Application.Products.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.ProductVariant.AddProductVariant
{
    public class AddProductVariantCommand : IBaseCommand
    {
        public long ProductId { get;  set; }
        public string SKU { get;  set; }
        public string? Color { get;  set; }
        public string? Size { get;  set; }
        public int StockQuantity { get;  set; }
        public decimal Price { get;  set; }
        public int? DiscountPercentage { get;  set; }
    }
}
