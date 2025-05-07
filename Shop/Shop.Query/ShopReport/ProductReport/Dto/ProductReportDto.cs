using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.ShopReport.ProductReport.Dto
{
    public class ProductReportDto
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public List<ProductVariantReportDto> Variants { get; set; }
    }
    public class ProductVariantReportDto
    {
        public long VariantId { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }

    }
}
