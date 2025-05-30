using Common.Query;
using Shop.Query.Products.DTOs;
using Shop.Query.Report.ProductReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.ProductReport.GetOutofStuckProduct
{
    public class GetOutOfStockProductsQuery:IQuery<List<OutOfStockProductDto>>
    {
    }
}
