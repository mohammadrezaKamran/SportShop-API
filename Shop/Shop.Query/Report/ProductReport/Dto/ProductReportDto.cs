using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.ProductReport.Dto
{

    public class ProductSalesDto
    {
        public long ProductId { get; set; }
        public string SKU { get; set; }
        public int TotalSold { get; set; }
    }
    public class OutOfStockProductDto
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public string Slug {  get; set; }
        public string SKU { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public int StockQuantity { get; set; }
    }
    
}
